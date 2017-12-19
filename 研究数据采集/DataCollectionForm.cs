/*   
 * by xy 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using System.Data.OleDb;

namespace LSD.Edit.Forms
{
    public partial class DataCollectionForm : Form
    {
        IMapControl3 pMapControl;
        string[] lines;//txt里读取出来的所有行,用这个避免读两次
        string toDelete;//待删除的文件路径（不包含扩展名）
        OleDbConnection m_conDBConnection;//连excel

        public DataCollectionForm(IMapControl3 mapcontrol)
        {
            pMapControl = mapcontrol;
            InitializeComponent();
        }

        #region Txt部分
        //选择要导入的文本文件
        private void btnOpenTxt_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "文本文件|*.txt";
            openFileDialog1.Multiselect = false;
            DialogResult pDialogResult = openFileDialog1.ShowDialog();
            if (pDialogResult != DialogResult.OK)
                return;
            this.textboxTxt.Text = openFileDialog1.FileName;
            //根据选择的文件的第一行来初始化combobox的待选项
            initComboTxt();
        }

        //初始化txt选项卡下的combobox
        private void initComboTxt()
        {
            try
            {
                if (textboxTxt.Text == String.Empty) return;
                lines = File.ReadAllLines(textboxTxt.Text, System.Text.Encoding.Default);
                string firstLine = lines[0];
                string[] fields = firstLine.Split(',');
                if (fields.Length < 3)//FID,TBBH,POINT_X,POINT_Y
                    MessageBox.Show("无法解析字段，请检查文件格式是否正确");
                foreach (string field in fields)
                {
                    cmbTBBHTxt.Items.Add(field);
                    cmbXTxt.Items.Add(field);
                    cmbYTxt.Items.Add(field);
                }
                cmbTBBHTxt.SelectedIndex = 1;
                cmbXTxt.SelectedIndex = 2;
                cmbYTxt.SelectedIndex = 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " 无法解析文件首行中的字段定义，请检查文件格式是否正确");
            }
        }

        //开始导入txt
        private void btnCollectTxt_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textboxTxt.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("坐标数据不能为空！");
                    return;
                }
                if (this.cmbXTxt.SelectedItem.ToString() == this.cmbYTxt.SelectedItem.ToString())
                {
                    MessageBox.Show("成图X字段与成图Y字段不能相同 ！");
                    return;
                }

                //一行一行解析，返回每个多边形的每个顶点
                List<List<Vertex>> polygons = ParseLines(lines);
                //创建一个新的featureclass，用来装文本文件里的那些多边形
                IFeatureClass newFeatureClass = GetNewFeatureClass(textboxTxt.Text, cmbTBBHTxt.SelectedItem.ToString());
                //添加读出来的那些多边形，也要添加属性
                CreateFeatures(newFeatureClass, polygons, cmbTBBHTxt.SelectedItem.ToString());
                //显示，把处理完成的要素类导进来
                ShowFeatures(newFeatureClass, System.IO.Path.GetFileName(textboxTxt.Text).Split('.')[0]);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //逐行解析
        private List<List<Vertex>> ParseLines(string[] lines)
        {
            string TBBH = "";
            List<List<Vertex>> polygons = new List<List<Vertex>>();
            List<Vertex> polygon = new List<Vertex>();
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] items = line.Split(',');
                //获取一个顶点
                Vertex v = new Vertex();
                v.TBBH = items[cmbTBBHTxt.SelectedIndex];
                v.X = Double.Parse(items[cmbXTxt.SelectedIndex]);
                v.Y = Double.Parse(items[cmbYTxt.SelectedIndex]);

                if (v.TBBH != TBBH)//图斑编号变了就把上一个polygon加到polygons里，顶点加到新的多边形里
                {
                    if (i != 1)//第一行避免添加空的polygon进去
                        polygons.Add(polygon);
                    TBBH = v.TBBH;
                    polygon = new List<Vertex>();
                    polygon.Add(v);
                }
                else//没变就在原来的列表里添加 
                {
                    polygon.Add(v);
                    if (i == lines.Length - 1)//最后一行，没有改变TBBH，要单独把最后一个polygon加到polygons里
                        polygons.Add(polygon);
                }
            }
            return polygons;
        }
        #endregion

        #region Excel部分
        //选择并打开excel文件
        private void btnOpenExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var OFD = new OpenFileDialog();
                OFD.Filter = "Excel数据|*.xls;*.xlsx";
                if (OFD.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                //获取选择Excel路径
                string strDBName = OFD.FileName;
                this.textboxExcel.Text = strDBName;
                this.textboxExcel.Tag = OFD.FilterIndex;
                //构造连接Excel字符串
                StringBuilder strConnect = new StringBuilder();
                string extension = System.IO.Path.GetExtension(strDBName);
                switch (extension)
                {
                    case ".xls":
                        //当为Excel03格式时
                        strConnect.Append(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};", strDBName));
                        strConnect.Append("Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'");
                        break;
                    case ".xlsx":
                        //当为Excel07格式时
                        //可能要装这个http://download.microsoft.com/download/7/0/3/703ffbcb-dc0c-4e19-b0da-1463960fdcdb/AccessDatabaseEngine.exe
                        strConnect.Append(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};", strDBName));
                        strConnect.Append("Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'");
                        break;
                    default:
                        break;
                }
                if (strConnect.ToString() == string.Empty)
                {
                    MessageBox.Show("打开Excel格式不支持！");
                    return;
                }
                this.cmbSheets.Items.Clear();
                m_conDBConnection = new OleDbConnection();
                m_conDBConnection.ConnectionString = strConnect.ToString();
                m_conDBConnection.Open();
                //获取Excel中sheet列表
                DataTable dtTable = m_conDBConnection.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new System.Object[] { null, null, null, "TABLE" });
                if (dtTable == null)
                {
                    MessageBox.Show("未能找到有效的Sheet表");
                    return;
                }
                //将获取到的Sheet列表添加到【选择坐标数据表】下拉列表中           
                foreach (System.Data.DataRow row in dtTable.Rows)
                {
                    string strTableName = row["TABLE_NAME"].ToString();
                    cmbSheets.Items.Add(strTableName);
                }
                cmbSheets.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //选中不同的数据表时根据首行改变字段选择的下拉框
        private void cmbSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.cmbTBBHExcel.Items.Clear();
                this.cmbXExcel.Items.Clear();
                this.cmbYExcel.Items.Clear();
                // 选择sheet表发生变化时，读取sheet表中列名加载到TBBH字段X字段Y字段
                //下拉选择列表
                string sheetTableName = cmbSheets.SelectedItem.ToString();
                DataTable dt = new DataTable();
                //读取前10条数据，返回数据结果DataTable
                dt = QueryBySql(String.Format("select top 10 * from [{0}]", sheetTableName));
                if (dt != null)
                {
                    //通过遍历加载X Y字段下拉选择框
                    foreach (DataColumn column in dt.Columns)
                    {
                        this.cmbTBBHExcel.Items.Add(column.ColumnName);
                        this.cmbXExcel.Items.Add(column.ColumnName);
                        this.cmbYExcel.Items.Add(column.ColumnName);
                    }
                    this.cmbTBBHExcel.SelectedIndex = 0;
                    this.cmbXExcel.SelectedIndex = 1;
                    this.cmbYExcel.SelectedIndex = 2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //开始导入excel
        private void btnCollectExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textboxExcel.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("坐标数据不能为空！");
                    return;
                }
                if (this.cmbXExcel.SelectedItem.ToString() == this.cmbYExcel.SelectedItem.ToString())
                {
                    MessageBox.Show("成图X字段与成图Y字段不能相同 ！");
                    return;
                }

                //获得所有数据
                DataTable dt = QueryBySql(String.Format("select * from [{0}]", this.cmbSheets.SelectedItem.ToString()));
                //一行一行解析，返回每个多边形的每个顶点
                List<List<Vertex>> polygons = ParseTable(dt);
                //创建一个新的featureclass，用来装文本文件里的那些多边形
                IFeatureClass newFeatureClass = GetNewFeatureClass(textboxExcel.Text, cmbTBBHExcel.SelectedItem.ToString());
                //添加读出来的那些多边形，也要添加属性
                CreateFeatures(newFeatureClass, polygons, cmbTBBHExcel.SelectedItem.ToString());
                //显示，把处理完成的要素类导进来
                ShowFeatures(newFeatureClass, System.IO.Path.GetFileName(textboxExcel.Text).Split('.')[0]);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        //excel的数据表一行一行读
        private List<List<Vertex>> ParseTable(DataTable dt)
        {
            string TBBH = "";
            List<List<Vertex>> polygons = new List<List<Vertex>>();
            List<Vertex> polygon = new List<Vertex>();
            string TBBHFieldName = cmbTBBHExcel.SelectedItem.ToString();
            string XFieldName = cmbXExcel.SelectedItem.ToString();
            string YFieldName = cmbYExcel.SelectedItem.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)//不包括字段名所在第一行
            {
                DataRow row = dt.Rows[i];
                //获取一个顶点
                Vertex v = new Vertex();
                v.TBBH = row[TBBHFieldName].ToString();
                v.X = Double.Parse(row[XFieldName].ToString());
                v.Y = Double.Parse(row[YFieldName].ToString());

                if (v.TBBH != TBBH)//图斑编号变了就把上一个polygon加到polygons里，顶点加到新的多边形里
                {
                    if (i != 0)//第一行避免添加空的polygon进去
                        polygons.Add(polygon);
                    TBBH = v.TBBH;
                    polygon = new List<Vertex>();
                    polygon.Add(v);
                }
                else//没变就在原来的列表里添加 
                {
                    polygon.Add(v);
                    if (i == dt.Rows.Count - 1)//最后一行，没有改变TBBH，要单独把最后一个polygon加到polygons里
                        polygons.Add(polygon);
                }
            }
            return polygons;
        }
        #endregion

        #region 通用的部分
        //多边形的顶点，对应于文件的一行
        public struct Vertex
        {
            public string TBBH;
            public double X, Y;
        }

        //新的要素类，装文本读出来的多边形，在当前打开的文件所在的文件夹下创建
        private IFeatureClass GetNewFeatureClass(string path, string TBBHFieldName)
        {
            //使用系统的临时文件夹作为工作空间
            string workspacePath = System.IO.Path.GetTempPath();//C:\Users\Xiao Yi\AppData\Local\Temp\
            workspacePath = workspacePath + "\\LSDAdminCreateFeatureClassTemp";
            if (!Directory.Exists(workspacePath)) 
            {
                Directory.CreateDirectory(workspacePath);//创建一个隐藏的临时文件夹，便于删除，这名字肯定不会重,其实不删都可以的
                File.SetAttributes(workspacePath, FileAttributes.Hidden);
            }
            toDelete = workspacePath;
            //打开工作空间
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(workspacePath, 0);
            //加上图斑编号字段
            IField field = new FieldClass();
            IFieldEdit fieldEdit = (IFieldEdit)field;
            fieldEdit.Name_2 = TBBHFieldName;
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            IField[] arrField = { field };
            
            //要素类的名称，避免已存在，否则会报错
            string fileName = System.IO.Path.GetFileName(path).Split('.')[0];
            while (File.Exists(workspacePath+"\\"+fileName + ".shp"))//如果已存在，就加长一点
            {
                fileName += "_副本";
            }            
            IFeatureClass newFeatureClass = CreateFeatureClass(fileName, pFeatureWorkspace, arrField);
            //系统关闭时，把临时创建的删掉    TODO合的时候记得改        
            MainForm.mainform.FormClosing += new System.Windows.Forms.FormClosingEventHandler(DeleteTempFiles);
            return newFeatureClass;
        }

        //关闭整个系统时删掉临时文件,其实不删都可以的，本来就是用的系统的Temp
        private void DeleteTempFiles(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Directory.Exists(toDelete))
                    Directory.Delete(toDelete, true);
            }
            catch (Exception ex) { }//可能有些删不了，比如锁住的，不要紧
        }

        //根据读取出来的坐标，创建新要素
        private void CreateFeatures(IFeatureClass targetClass, List<List<Vertex>> polygons, string TBBHFieldName)
        {
            //编辑器
            IWorkspaceEdit m_WorkspaceEdit = (targetClass as IDataset).Workspace as IWorkspaceEdit;
            m_WorkspaceEdit.StartEditing(true);
            m_WorkspaceEdit.StartEditOperation();

            //一个一个多边形地处理，对应一个要素
            foreach (List<Vertex> polygon in polygons)
            {
                IFeature newFeature = targetClass.CreateFeature();
                Ring ring = new RingClass();
                object missing = Type.Missing;
                //把每个顶点添加到环
                foreach (Vertex vertex in polygon)
                {
                    IPoint pt = new PointClass();
                    double X = vertex.X;
                    double Y = vertex.Y;
                    pt.PutCoords(X, Y);
                    ring.AddPoint(pt, ref missing, ref missing);
                }
                //投影
                IGeometry geometry = ring as IGeometry;
                IGeoDataset pGeoDataset = targetClass as IGeoDataset;
                if (pGeoDataset.SpatialReference != null)
                {
                    geometry.Project(pGeoDataset.SpatialReference);
                }
                else
                {
                    geometry.Project(pMapControl.SpatialReference);
                }
                //环构成多边形
                IGeometryCollection newPolygon = new PolygonClass();
                newPolygon.AddGeometry(geometry, ref missing, ref missing);
                IPolygon newPolygon2 = newPolygon as IPolygon;
                newPolygon2.SimplifyPreserveFromTo();
                newFeature.Shape = newPolygon2 as IGeometry;
                //加上TBBH属性
                int fieldIndex = newFeature.Fields.FindField(TBBHFieldName);
                newFeature.set_Value(fieldIndex, polygon[0].TBBH);//取任意一个顶点的tbbh
                newFeature.Store();
            }
            //保存
            m_WorkspaceEdit.StopEditOperation();
            m_WorkspaceEdit.StopEditing(true);
            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.FeatureClass = targetClass;
        }

        //把新创建并添加了读取出的要素的要素类加到地图里
        private void ShowFeatures(IFeatureClass newFeatureClass, string name)
        {
            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.FeatureClass = newFeatureClass;
            pFeatureLayer.Name = name;
            pMapControl.AddLayer(pFeatureLayer);
            pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, pFeatureLayer.AreaOfInterest);
        }

        //用帮助里的例子，修改了字段和空间参考
        public IFeatureClass CreateFeatureClass(String featureClassName, IFeatureWorkspace featureWorkspace, IField[] arrFields)
        {
            // Instantiate a feature class description to get the required fields.
            IFeatureClassDescription fcDescription = new FeatureClassDescriptionClass();
            IObjectClassDescription ocDescription = (IObjectClassDescription)fcDescription;
            IFields fields = ocDescription.RequiredFields;
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;

            // Add some fields to the required fields.
            foreach (IField pField in arrFields)
            {
                fieldsEdit.AddField(pField);
            }

            // Find the shape field in the required fields and modify its GeometryDef to
            // use point geometry and to set the spatial reference.
            int shapeFieldIndex = fields.FindField(fcDescription.ShapeFieldName);
            IField field = fields.get_Field(shapeFieldIndex);
            IGeometryDef geometryDef = field.GeometryDef;
            IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            geometryDefEdit.SpatialReference_2 = this.pMapControl.SpatialReference;//导进来的要素类使用当前地图的空间参考

            // Use IFieldChecker to create a validated fields collection.
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IEnumFieldError enumFieldError = null;
            IFields validatedFields = null;
            fieldChecker.ValidateWorkspace = (IWorkspace)featureWorkspace;
            fieldChecker.Validate(fields, out enumFieldError, out validatedFields);

            // The enumFieldError enumerator can be inspected at this point to determine 
            // which fields were modified during validation.

            // Create the feature class.//这一步极易出错，一般都是名为featureClassName的文件已存在
            IFeatureClass featureClass = featureWorkspace.CreateFeatureClass(featureClassName, validatedFields,
                ocDescription.InstanceCLSID, ocDescription.ClassExtensionCLSID, esriFeatureType.esriFTSimple,
                fcDescription.ShapeFieldName, "");
            return featureClass;
        }

        /// <summary>
        /// sql查询返回table形式
        /// </summary>
        /// <param name="p_strSql"></param>
        /// <returns></returns>
        private DataTable QueryBySql(string p_strSql)
        {
            //构造数据库操作变量，利用sql查询返回DataTable形式
            OleDbCommand m_cmdCommand = new OleDbCommand();
            m_cmdCommand.Connection = m_conDBConnection;
            m_cmdCommand.CommandText = p_strSql;
            m_cmdCommand.CommandType = CommandType.Text;
            //进行数据查询
            using (OleDbDataAdapter m_dtrAdapter = new OleDbDataAdapter(m_cmdCommand))
            {
                DataSet objDs = new DataSet();
                m_dtrAdapter.Fill(objDs);
                return objDs.Tables[0];
            }
        }
        #endregion

        #region CAD部分
        private void btnCollectCAD_Click(object sender, EventArgs e)
        {
            if (this.textboxCAD.Text == "")
            {
                return;
            }
            try
            {
                //获取文件名和文件路径
                int pIndex = this.textboxCAD.Text.LastIndexOf("\\");
                string pFilePath = this.textboxCAD.Text.Substring(0, pIndex);
                string pFileName = this.textboxCAD.Text.Substring(pIndex + 1);
                //打开CAD数据集
                IWorkspaceFactory pWorkspaceFactory;
                IFeatureWorkspace pFeatureWorkspace;
                IFeatureLayer pFeatureLayer;
                IFeatureDataset pFeatureDataset;
                pWorkspaceFactory = new CadWorkspaceFactoryClass(); //using ESRI.ArcGIS.DataSourcesFile;
                pFeatureWorkspace = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(pFilePath, 0);
                //打开一个要素集
                pFeatureDataset = pFeatureWorkspace.OpenFeatureDataset(pFileName);
                //IFeatureClassContainer可以管理IFeatureDataset中的每个要素类
                IFeatureClassContainer pFeatClassContainer = (IFeatureClassContainer)pFeatureDataset;
                IGroupLayer pGroupLayer = new GroupLayerClass();
                pGroupLayer.Name = pFileName;
                //对CAD文件中的要素进行遍历处理
                for (int i = 0; i < pFeatClassContainer.ClassCount; i++)
                {
                    IFeatureClass pFeatClass = pFeatClassContainer.get_Class(i);
                    //如果是注记，则添加注记层
                    if (pFeatClass.FeatureType == esriFeatureType.esriFTCoverageAnnotation)
                    {
                        pFeatureLayer = new CadAnnotationLayerClass();
                        pFeatureLayer.Name = pFeatClass.AliasName;
                        pFeatureLayer.FeatureClass = pFeatClass;
                        pGroupLayer.Add(pFeatureLayer);
                        //this.pMapControl.Map.AddLayer(pFeatureLayer);
                    }
                    else //如果是点、线、面则添加要素层
                    {
                        pFeatureLayer = new FeatureLayerClass();
                        pFeatureLayer.Name = pFeatClass.AliasName;
                        pFeatureLayer.FeatureClass = pFeatClass;
                        pGroupLayer.Add(pFeatureLayer);
                        //this.pMapControl.Map.AddLayer(pFeatureLayer);
                    }
                }
                this.pMapControl.Map.AddLayer(pGroupLayer);
                this.pMapControl.ActiveView.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOpenCAD_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.Filter = "CAD(*.dwg)|*.dwg";
            pOpenFileDialog.Title = "打开CAD数据文件";
            pOpenFileDialog.ShowDialog();

            this.textboxCAD.Text = pOpenFileDialog.FileName;
        }
        #endregion
    }
}

/*
 * 形如
FID,TBBH,POINT_X,POINT_Y
0,1051,40455314.974799998000000,3581584.076100000200000
1,1051,40455298.590800002000000,3581579.660399999900000
2,1051,40455297.243600003000000,3581670.486500000100000
3,1051,40455580.976499997000000,3581742.740900000100000
4,1051,40455587.660599999000000,3581653.652799999800000
......
18,767,40454772.833800003000000,3583983.055300000100000
19,767,40454765.343599997000000,3583721.196300000000000
20,767,40454748.386299998000000,3583721.664100000200000
21,767,40454702.430500001000000,3583722.402999999900000
22,767,40454693.933100000000000,3583720.892800000000000
23,767,40454689.684299998000000,3583719.571399999800000
......
 * 的txt或excel
 */
