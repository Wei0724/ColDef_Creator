﻿using System.Diagnostics.Metrics;
using System.DirectoryServices;
using System.Security.AccessControl;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using System.Reflection;
using System;
using System.Diagnostics;
using ListView = System.Windows.Forms.ListView;
using System.Data;
using System.Collections.Generic;
using static WinForms.ColDef_Creator;
using System.Text;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json.Linq;
using ComboBox = System.Windows.Forms.ComboBox;
using System.ComponentModel.DataAnnotations;

namespace WinForms
{
    public partial class ColDef_Creator : Form
    {

        List<SearchResult> mainFilteredList = new List<SearchResult>();

        List<SearchResult> LOVFilteredList = new List<SearchResult>();

        List<innerObj> list_innerObj = new List<innerObj>();

        List<innerObj> list_LovObj = new List<innerObj>();

        public int count = 0;

        private string filePath = "";

        private int tempColumnIndex = 0;

        private int tempRowIndex = 0;

        ListViewItem lvi;


        public class innerObj
        {

            public int innerIndex { get; set; }    //內部索引
            public int index { get; set; }    //分類索引
            public string dbColumn { get; set; }    //* 資料欄名稱
            public string itemEngName { get; set; } //* 名稱
            public string itemChtName { get; set; } //* 標籤 or * 提示(first)
            public string itemType { get; set; }    //* 項目類型

            public string HeaderType { get; set; }    //類型Gird/Form

            public int? SubSeq { get; set; }    //順序索引

            public bool Checked { get; set; }    //選取

        }

        public class SearchResult
        {
            public int Index { get; }
            public string Line { get; }
            public string Header { get; }
            public string Sub { get; }
            public int index { get; }

            public string HeaderType { get; set; }

            public int? SubSeq { get; set; }



            public SearchResult(int index, string line)
            {
                Index = index;
                Line = line;
            }

            public SearchResult(int Counter, string line1, string line2, string type, int? subSeq)
            {
                index = Counter;
                Header = line1;
                Sub = line2;
                HeaderType = type;
                SubSeq = subSeq;
            }




        }

        private ContextMenuStrip contextMenu;//宣告一個右鍵選單的物件





        public ColDef_Creator()
        {
            InitializeComponent();
            InitialListView();
            //InitailComboBoxColumn();
            InitialContextMenu();

            //版本號
            string version = System.Windows.Forms.Application.ProductVersion;
            this.Text = String.Format("ColDef Creator {0}", version);

        }



        private void InitialListView()
        {
            //lstVwHeaders.View = View.Details;
            //lstVwHeaders.GridLines = true;
            //lstVwHeaders.LabelEdit = true;
            //lstVwHeaders.FullRowSelect = true;
            //lstVwHeaders.Columns.Add("編號", 100);
            //lstVwHeaders.Columns.Add("Grid/Form", 100);

            //for (int i = 0; i < 10; i++)
            //{
            //    var item = new ListViewItem($"No.{i}");
            //    item.SubItems.Add($"文字{i}");
            //    lstVwHeaders.Items.Add(item);
            //}

            // Set the view to show details.
            lstVwSubItems.View = View.Details;
            // Allow the user to edit item text.
            //lstVwSubItems.LabelEdit = true;
            // Allow the user to rearrange columns.
            lstVwSubItems.AllowColumnReorder = true;
            // Display check boxes.
            lstVwSubItems.CheckBoxes = true;
            // Select the item and subitems when selection is made.
            lstVwSubItems.FullRowSelect = true;
            // Display grid lines.
            lstVwSubItems.GridLines = true;
            // Sort the items in the list in ascending order.
            lstVwSubItems.Sorting = SortOrder.Ascending;

            // Create three items and three sets of subitems for each item.
            //ListViewItem item1 = new ListViewItem("item1", 0);
            //// Place a check mark next to the item.
            //item1.Checked = true;
            //item1.SubItems.Add("1");
            //item1.SubItems.Add("2");
            //item1.SubItems.Add("3");
            //ListViewItem item2 = new ListViewItem("item2", 1);
            //item2.SubItems.Add("4");
            //item2.SubItems.Add("5");
            //item2.SubItems.Add("6");
            //ListViewItem item3 = new ListViewItem("item3", 0);
            //// Place a check mark next to the item.
            //item3.Checked = true;
            //item3.SubItems.Add("7");
            //item3.SubItems.Add("8");
            //item3.SubItems.Add("9");

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            //lstVwSubItems.Columns.Add("Item Column", -2, HorizontalAlignment.Left);
            //lstVwSubItems.Columns.Add("Column 2", -2, HorizontalAlignment.Left);
            //lstVwSubItems.Columns.Add("Column 3", -2, HorizontalAlignment.Left);
            //lstVwSubItems.Columns.Add("Column 4", -2, HorizontalAlignment.Center);

            //Add the items to the ListView.
            //lstVwSubItems.Items.AddRange(new ListViewItem[] { item1, item2, item3 });

            // Create two ImageList objects.
            ImageList imageListSmall = new ImageList();
            ImageList imageListLarge = new ImageList();

            // Initialize the ImageList objects with bitmaps.
            //imageListSmall.Images.Add(Bitmap.FromFile("d:\\Users\\Wei_Pan\\Desktop\\ERP1.jpg"));
            //imageListSmall.Images.Add(Bitmap.FromFile("d:\\Users\\Wei_Pan\\Pictures\\狗幣.png"));
            //imageListLarge.Images.Add(Bitmap.FromFile("d:\\Users\\Wei_Pan\\Pictures\\柴01.jpg"));
            //imageListLarge.Images.Add(Bitmap.FromFile("d:\\Users\\Wei_Pan\\Pictures\\柴03.jpg"));


            imageListSmall.Images.Add(Bitmap.FromFile("ERP1.jpg"));
            imageListSmall.Images.Add(Bitmap.FromFile("狗幣.png"));
            imageListLarge.Images.Add(Bitmap.FromFile("柴01.jpg"));
            imageListLarge.Images.Add(Bitmap.FromFile("柴03.jpg"));

            //Assign the ImageList objects to the ListView.
            lstVwSubItems.LargeImageList = imageListLarge;
            lstVwSubItems.SmallImageList = imageListSmall;

            this.comboBox1.Visible = false;

        }

        //設定右鍵選單
        private void InitialContextMenu()
        {
            // 加載圖像
            Image image1 = Image.FromFile(@"delete.png");
            Image image2 = Image.FromFile(@"form.png");
            Image image3 = Image.FromFile(@"grid.png");


            //if (contextMenu == null)
            //{
            contextMenu = new ContextMenuStrip();
            // 創建菜單項目
            ToolStripMenuItem item1 = new ToolStripMenuItem("清空", image1);
            ToolStripMenuItem item2 = new ToolStripMenuItem("設定Form", image2);
            ToolStripMenuItem item3 = new ToolStripMenuItem("設定Grid", image3);

            // 將菜單項目添加到ContextMenuStrip
            //contextMenu.Items.Add(item1);
            //contextMenu.Items.Add(item2);
            //contextMenu.Items.Add(item3);
            contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { item1, item2, item3 });

            //}

            // 關聯ContextMenuStrip到您的控制元件
            this.dGdVwHeaders.Columns[1].ContextMenuStrip = contextMenu;

            //item1.Click += new EventHandler(toolstrip_Click);

            // 設置事件處理程序以處理選項的點擊事件
            item1.Click += Item1_Click;
            item2.Click += Item2_Click;
            item3.Click += Item3_Click;
        }

        //設定DataGridView下拉式選單item
        private void InitailComboBoxColumn()
        {
            // 創建一個DataGridViewComboBoxColumn
            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();

            // 設定列的名稱
            comboBoxColumn.Name = "Type";
            comboBoxColumn.HeaderText = "Model類型";

            // 設定下拉式選單的選項
            comboBoxColumn.Items.Add("");
            comboBoxColumn.Items.Add("Form");
            comboBoxColumn.Items.Add("Grid");

            // 设置单元格的默认样式
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.ForeColor = System.Drawing.Color.Blue;
            cellStyle.BackColor = System.Drawing.Color.Yellow;
            comboBoxColumn.DefaultCellStyle = cellStyle;

            // 將列添加到DataGridView中
            dGdVwHeaders.Columns.Add(comboBoxColumn);
        }




        private void btnPath_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                // 設定預設的檔案類型篩選器
                //openFileDialog.Filter = "所有檔案|*.*";
                openFileDialog.Filter = "文本檔案 (*.txt)|*.txt";
                openFileDialog.Title = "選擇要打開的檔案";

                // 使用者選擇了檔案
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    list_innerObj.Clear();
                    list_LovObj.Clear();

                    string selectedFilePath = openFileDialog.FileName;
                    this.txtSelectPath.Text = selectedFilePath;

                    //textBoxResults.AppendText($" 取得檔名(不包含附檔名)： {Path.GetFileNameWithoutExtension(openFileDialog.FileName)}]{Environment.NewLine}");
                    filePath = openFileDialog.FileName; //(全域)



                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    string[] lines;

                    //// 檢測文件編碼

                    bool is_Big5 = IsBig5Encoding(openFileDialog.FileName);
                    Encoding txt_encoding = DetectEncoding(openFileDialog.FileName);
                    //textBoxResults.AppendText($"is_Big5:{is_Big5}{Environment.NewLine}");

                    // 判斷編碼
                    if (!IsBig5Encoding(openFileDialog.FileName))
                    {
                        //MessageBox.Show("非BIG5編碼");
                        textBoxResults.AppendText($"非BIG5編碼{Environment.NewLine}");
                        // 讀取文字檔的每一行
                        lines = File.ReadAllLines(selectedFilePath);
                    }
                    else
                    {
                        // 讀取文字檔的每一行
                        lines = File.ReadAllLines(selectedFilePath, Encoding.GetEncoding("BIG5"));
                        //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        // 將每行內容從預設編碼轉換為 UTF-8
                        for (int i = 0; i < lines.Length; i++)
                        {
                            //byte[] encodedBytes = Encoding.Default.GetBytes(lines[i]);

                            Encoding big5Encoding = Encoding.GetEncoding("BIG5");
                            byte[] bytes = big5Encoding.GetBytes(lines[i]);

                            byte[] encodedBytes = Encoding.Convert(Encoding.GetEncoding("BIG5"), Encoding.UTF8, bytes);
                            string utf8Line = Encoding.UTF8.GetString(encodedBytes);
                            lines[i] = utf8Line;
                            //textBoxResults.AppendText($"{lines[i]}{Environment.NewLine}");
                        }
                    }



                    //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    //byte[] big5Bytes = null;
                    //using (System.IO.FileStream fs = new System.IO.FileStream(selectedFilePath, System.IO.FileMode.Open))
                    //{
                    //    //讀big5編碼bytes
                    //    big5Bytes = new byte[fs.Length];
                    //    fs.Read(big5Bytes, 0, (int)fs.Length);
                    //}

                    ////將big5轉成utf8編碼的bytes
                    //byte[] utf8Bytes = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding("BIG5"), System.Text.Encoding.UTF8, big5Bytes);

                    ////將utf8 bytes轉成utf8字串
                    //System.Text.UTF8Encoding encUtf8 = new System.Text.UTF8Encoding();

                    //string utf8Str = encUtf8.GetString(utf8Bytes);

                    //textBoxResults.AppendText($"{utf8Str}");








                    //// 要搜尋的字串
                    //string searchString = " * 區塊                                              ";

                    // 要搜尋的多個字串
                    string[] searchStrings = new string[]
                    {
                    " * 區塊                                              ",
                    "   * 項目                                            ",
                    "     * 名稱                                          ",
                    "     * 項目類型                                      ",
                    "     - 項目類型                                      ",
                    "     ^ 項目類型                                      ",
                    "     * 資料欄名稱                                    ",
                    "     * 提示                                          ",
                    "   - 關係                                            ",
                    "     * 標籤                                          ",
                    " * Blocks                                            ",
                    "   * Items                                           ",
                    "     * Name                                          ",
                    "     * Item Type                                     ",
                    "     - Item Type                                     ",
                    //"     ^ Item Type                                     ",
                    "     * Column Name                                   ",
                    "     * Prompt                                        ",
                    "   - Relations                                       ",
                    "     ^ Label                                         ",
                    " * 值清單                                            ",   // Header
                    " * Lists of Values                                   ",   // Header
                    " - 功能表                                            ",
                    " - Menus                                             ",
                    "* 來源名稱                                  LOV_ITEM",
                    "   * 記錄群組                                        ",
                    "     * 值清單                                        LOV_",    //與 * 資料欄名稱 同層
                    "     * List of Values                                LOV_",    //與 * Column Name  同層
                    "* 名稱                                            LOV_",       //與 * 值清單 字串一致
                    "* Name                                            LOV_",       //與 * List of Values 字串一致
                    "     * 名稱                                          ",        // LOV MAPPING
                    "     * 標題                                          ",        // LOV MAPPING
                    "     * 回覆項目                                      ",        // LOV MAPPING
                    "     * 顯示寬度                                      ",        // LOV MAPPING
                    "     * Name                                          ",        // LOV MAPPING
                    "     * Title                                         ",        // LOV MAPPING
                    "     * Return Item                                   ",        // LOV MAPPING
                    "     * Display Width                                 ",        // LOV MAPPING
                    };

                    // 儲存符合搜尋字串的索引和字串
                    var searchResults = new List<SearchResult>();

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string line = lines[i];


                        foreach (string searchString in searchStrings)
                        {
                            // 判斷是否有符合搜尋字串的內容
                            if (line.Contains(searchString))
                            {
                                // 建立 SearchResult 物件並將索引和字串儲存起來
                                var result = new SearchResult(i, line);
                                searchResults.Add(result);
                                break; // 找到符合的字串後跳出迴圈，繼續處理下一行
                            }
                        }
                    }

                    //var lines2 = new List<string>();

                    //// 使用搜尋結果進行後續操作，例如輸出到控制台或顯示在視窗中
                    //foreach (var result in searchResults)
                    //{
                    //    lines2.Add($"找到符合的字串：索引：{result.Index}，內容：{result.Line}");
                    //    textBoxResults.AppendText($"000 找到符合的字串：內容：[{result.Line}]，索引：{result.Index}{Environment.NewLine}");
                    //}
                    //this.textBoxResults.Text = lines2.;



                    // 要搜尋的多個字串 Header
                    string[] headerStrings = new string[]
                    {
                    " * 區塊                                              ",
                    "   * 項目                                            ",
                    "   - 關係                                            ",
                    " * Blocks                                            ",
                    "   * Items                                           ",
                    "   - Relations                                       ",
                    " * 值清單                                            ",
                    " * Lists of Values                                   ",
                    " - 功能表                                            ",
                    " - Menus                                             ",
                    };


                    // 儲存分類後的結果
                    //var categorizedResults = new Dictionary<string, List<string>>();
                    var categorizedResults = new List<SearchResult>();

                    // 將搜尋結果顯示在 TextBox 控制項中
                    //textBoxResults.Clear();
                    string header = null;
                    int counter = 0;
                    foreach (var result in searchResults)
                    {
                        //textBoxResults.AppendText($"001 找到符合的字串：內容：[{result.Line}]，索引：{result.Index}{Environment.NewLine}");


                        for (int i = 0; i < headerStrings.Length; i++)
                        {
                            string line = headerStrings[i];


                            // 判斷是否有符合搜尋字串的內容
                            if (result.Line.Contains(line))
                            {
                                // 如果是頭分類，則設定目前的分類為該頭分類
                                header = result.Line;
                                counter++;
                            }

                        }

                        if (!string.IsNullOrEmpty(header) && header != result.Line)
                        {
                            // 如果是子分類，將該行文字加入到目前的分類中
                            // 建立 SearchResult 物件並將索引和字串儲存起來
                            categorizedResults.Add(new SearchResult(counter, header, result.Line, "", null));
                        }
                    }


                    //textBoxResults.Clear();
                    //foreach (var obj in resultobj)
                    //{
                    //    textBoxResults.AppendText($"003 找到符合的字串：Counter：[{obj.index}]，頭：[{obj.Header}]，子：[{obj.Sub}]{Environment.NewLine}");
                    //}


                    /* 過濾掉'非項目'的分類 */
                    // 主層
                    this.mainFilteredList = categorizedResults.FindAll(e => e.Header == "   * 項目                                            "
                        || e.Header == "   * Items                                           ");

                    // LOV層
                    this.LOVFilteredList = categorizedResults.FindAll(e => e.Header == " * 值清單                                            "
                        || e.Header == " * Lists of Values                                   ");


                    int item_counter = 0;
                    int temp_index = 0;
                    bool flag = false;
                    //textBoxResults.Clear();
                    string _temp_dbColumn = string.Empty, _temp_itemEngName = string.Empty, _temp_itemChtName = string.Empty, _temp_itemType = string.Empty;

                    string _LOV_dbColumn = string.Empty, _LOV_itemEngName = string.Empty, _LOV_itemChtName = string.Empty, _LOV_key = string.Empty;



                    // LOV層 填值處理
                    foreach (var obj in LOVFilteredList.Select((Value, Index) => new { Value, Index }))
                    {
                        //textBoxResults.AppendText($"LOV找到符合的字串：Counter：[{obj.Value.index}]，頭：[{obj.Value.Header}]，子：[{obj.Value.Sub}]{Environment.NewLine}");


                        if (obj.Value.Sub.Contains("     * 名稱                                          ")
                            || obj.Value.Sub.Contains("     * Name                                          "))
                            _LOV_itemEngName = getRealValue(obj.Value.Sub);



                        if (obj.Value.Sub.Contains("* 回覆項目") || obj.Value.Sub.Contains("* Return Item"))
                            _LOV_dbColumn = getRealValue(obj.Value.Sub);


                        if (obj.Value.Sub.Contains("* 標題") || obj.Value.Sub.Contains("* Title"))
                            _LOV_itemChtName = getRealValue(obj.Value.Sub);


                        if (obj.Value.Sub.Contains("* 名稱                                            LOV_")
                            || obj.Value.Sub.Contains("* Name                                            LOV_"))
                            _LOV_key = getRealValue(obj.Value.Sub);


                        if (obj.Value.Sub.Contains("* 顯示寬度")
                            || obj.Value.Sub.Contains("* Display Width"))
                        {
                            //加入'主要物件'到'list_LovObj'
                            list_LovObj.Add(new innerObj()
                            {
                                HeaderType = "",
                                SubSeq = null,
                                index = flag ? temp_index : obj.Value.index,
                                dbColumn = _LOV_dbColumn,
                                itemEngName = _LOV_itemEngName,
                                itemChtName = _LOV_itemChtName,
                                itemType = _LOV_key,
                            });
                        }

                    }

                    //// LOV物件列表
                    //foreach (var obj in list_LovObj)
                    //    textBoxResults.AppendText($"LOV字串：index：[{obj.index}]，itemType：[{obj.itemType}]，dbColumn：[{obj.dbColumn}]，itemEngName：[{obj.itemEngName}]，itemChtName：[{obj.itemChtName}]{Environment.NewLine}");


                    // 主層 填值處理
                    foreach (var obj in mainFilteredList.Select((Value, Index) => new { Value, Index }))
                    {
                        textBoxResults.AppendText($"找到符合的字串：Counter：[{obj.Value.index}]，頭：[{obj.Value.Header}]，子：[{obj.Value.Sub}]{Environment.NewLine}");




                        if (obj.Value.Sub.Contains("* 名稱")
                            || obj.Value.Sub.Contains("* Name"))
                        {
                            //if (string.Empty != _temp_dbColumn && string.Empty != _temp_itemEngName && string.Empty != _temp_itemChtName && string.Empty != _temp_itemType)
                            //if (flag == true)
                            //if (item_counter == 4)
                            if (_temp_itemEngName != string.Empty)
                            {
                                //加入'主要物件'到'主要objectlist'
                                list_innerObj.Add(new innerObj()
                                {
                                    HeaderType = "",
                                    SubSeq = null,
                                    index = flag ? temp_index : obj.Value.index,
                                    dbColumn = _temp_dbColumn,
                                    itemEngName = _temp_itemEngName,
                                    itemChtName = _temp_itemChtName,
                                    itemType = _temp_itemType
                                });

                                _temp_dbColumn = string.Empty; _temp_itemEngName = string.Empty; _temp_itemChtName = string.Empty; _temp_itemType = string.Empty;
                                //item_counter = 0;
                            }

                            _temp_itemEngName = getRealValue(obj.Value.Sub);
                            //flag = true;
                            //item_counter++;
                        }

                        if (obj.Value.Sub.Contains("* 資料欄名稱")
                            || obj.Value.Sub.Contains("* Column Name"))
                        {
                            _temp_dbColumn = getRealValue(obj.Value.Sub);
                            //item_counter++;
                        }


                        if (obj.Value.Sub.Contains("* 標籤") || obj.Value.Sub.Contains("* 提示")
                            || obj.Value.Sub.Contains("* Prompt") || obj.Value.Sub.Contains("^ Label")
                            && string.Empty == _temp_itemChtName)
                        {
                            _temp_itemChtName = getRealValue(obj.Value.Sub);
                            //item_counter++;
                        }


                        if (obj.Value.Sub.Contains("* 項目類型")
                            || obj.Value.Sub.Contains("* Item Type")
                            || obj.Value.Sub.Contains("^ 項目類型")
                            || obj.Value.Sub.Contains("^ Item Type")
                            || obj.Value.Sub.Contains("- 項目類型")
                            || obj.Value.Sub.Contains("- Item Type"))
                        {
                            _temp_itemType = getRealValue(obj.Value.Sub);
                            //item_counter++;
                        }


                        //20231003 add
                        if (obj.Value.Sub.Contains("     * 值清單                                        LOV_")
                             || obj.Value.Sub.Contains("     * List of Values                                LOV_"))
                        {
                            _temp_itemType = getRealValue(obj.Value.Sub);
                            //item_counter++;
                        }


                        //分類索引切換之際
                        if (obj.Value.index != temp_index)
                        {
                            for (int i = 0; i < 5; i++)
                                list_innerObj.Add(new innerObj()
                                {
                                    HeaderType = "",
                                    SubSeq = null,
                                    index = obj.Value.index,
                                    dbColumn = null,
                                    itemEngName = null,
                                    itemChtName = null,
                                    itemType = "Form隱藏格式"
                                });

                            //list_innerObj.Add(new innerObj(){ dbColumn=_temp_dbColumn , itemEngName  = _temp_itemEngName , itemChtName  = _temp_itemChtName , itemType=_temp_itemType });
                            temp_index = obj.Value.index;
                            flag = true;
                        }



                        // 最後一筆額外處理
                        if (mainFilteredList.Count == obj.Index + 1)
                        {
                            //加入'主要物件'到'主要objectlist'
                            list_innerObj.Add(new innerObj()
                            {
                                HeaderType = "",
                                SubSeq = null,
                                index = obj.Value.index,
                                dbColumn = _temp_dbColumn,
                                itemEngName = _temp_itemEngName,
                                itemChtName = _temp_itemChtName,
                                itemType = _temp_itemType
                            });
                        }



                    }




                    foreach (var obj in list_innerObj.Select((Value, Index) => new { Value, Index }))
                        obj.Value.innerIndex = obj.Index;



                    if (list_innerObj.Count > 0)
                        dGV_innerObj.DataSource = list_innerObj;
                    //textBoxResults.AppendText($"{JsonConvert.SerializeObject(list_innerObj)}");





                    //BindingSource bs = new BindingSource();

                    this.dGdVwHeaders.DataSource = null;
                    this.dGdVwHeaders.Rows.Clear();


                    List<IGrouping<int, innerObj>> headerlist = list_innerObj.GroupBy(x => x.index).ToList();//取header使用

                    headerlist.ForEach(group =>
                    {
                        //textBoxResults.AppendText($"Score:{group.Key}, Count:{group.Count()}{Environment.NewLine}");
                        //this.dGdVwHeaders.Rows.Add(group.Key, "Grid/Form");
                        this.dGdVwHeaders.Rows.Add(group.Key, "");
                        //group.ToList().ForEach(item => textBoxResults.AppendText($"\tindex:{item.index}"));//, Score:{item.Score}  分組後內容取得
                    });


                    var datarow = this.dGdVwHeaders.Rows[0];
                    this.Fun_Reload_SubDataGridView(datarow);





















                    //string _templateModel = "      {{\r\n        headerName: '{0}',\r\n        headerValueGetter: this._agService.headerValueGetter,\r\n        field: '{1}',\r\n        type: 'text',\r\n        editable: false,\r\n        width: 100,\r\n        suppressSizeToFit: true,\r\n      }},\r\n";
                    //string _templateModel2 = "      new FormTextBox({{\r\n        key: '{1}',\r\n        label: '{0}',\r\n        // labelWidth: 5,\r\n        flex: '20',\r\n        order: 1,\r\n        class: 'pr-1',\r\n        // readonly: true,\r\n        // textAlign: 'right',\r\n      }}),\r\n";
                    //string resultStr = string.Empty;
                    //List<string> list_resultStr = new List<string>();

                    //foreach (innerObj obj in list_innerObj)
                    //{

                    //    if (obj.dbColumn == null && obj.itemEngName != null)
                    //        obj.dbColumn = obj.itemEngName;

                    //    //textBoxResults.AppendText(String.Format(_templateModel2, obj.itemChtName, obj.dbColumn));
                    //    list_resultStr.Add(String.Format(_templateModel2, obj.itemChtName, obj.dbColumn));

                    //}


                    //foreach (var resultStr2 in list_resultStr)
                    //    textBoxResults.AppendText(resultStr2);

                }

                //FolderBrowserDialog path = new FolderBrowserDialog();
                //path.ShowDialog();
                //this.txtSelectPath.Text = path.SelectedPath;
                ////取得模組列表
                //List<string> ds = fhelper.DirSearch(this.txtSelectPath.Text + AppPortalpath);

                //BindingSource bs = new BindingSource();
                ////放入from_module
                //this.cbx_frommodule.DataSource = ds;
                ////放入target_module
                //this.cbx_targetmodule.DataSource = ds;


            }
            catch (Exception e1)
            {
                throw e1;
            }
        }

        public string getRealValue(string line)
        {
            string key, value = null;

            string[] parts = line.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                key = parts[0].Trim();
                value = parts[1].Trim();

            }
            return value;
        }



        //偵測byte[]是否為BIG5編碼
        public static bool IsBig5Encoding(byte[] bytes)
        {
            Encoding big5 = Encoding.GetEncoding(950);
            //將byte[]轉為string再轉回byte[]看位元數是否有變
            return bytes.Length ==
                big5.GetByteCount(big5.GetString(bytes));
        }
        //偵測檔案否為BIG5編碼
        public static bool IsBig5Encoding(string file)
        {
            if (!File.Exists(file)) return false;
            return IsBig5Encoding(File.ReadAllBytes(file));
        }

        // 編碼檢測函數
        private static Encoding DetectEncoding(string filePath)
        {
            //using (StreamReader reader = new StreamReader(filePath, true))
            //{
            //    //reader.Peek(); // 確保流的讀取器內部緩衝區被填充
            //    string content = reader.ReadToEnd();

            //    return reader.CurrentEncoding;
            //}

            using (StreamReader reader = new StreamReader(filePath, Encoding.Default))
            {
                reader.Peek(); // 確保流的讀取器內部緩衝區被填充
                string content = reader.ReadToEnd();

                // MessageBox.Show($"編碼:{Encoding.Default.EncodingName}{Environment.NewLine}");

                return reader.CurrentEncoding;
            }
        }



        //點擊DataGridView(GRID/FORM)
        private void dGdVwHeaders_SelectionChanged(object sender, EventArgs e)
        {
            var rowsCount = dGdVwHeaders.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1) return;

            var datarow = dGdVwHeaders.SelectedRows[0];
            this.Fun_Reload_SubDataGridView(datarow);


            //Add the items to the ListView.
            //lstVwSubItems.Items.AddRange(new ListViewItem[] { item1, item2, item3 });

        }

        //選細項順序  重新載入也會觸發
        private void lstVwSubItems_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //textBoxResults.AppendText($"{JsonConvert.SerializeObject(e)}");

            //由目前點選的資料去反取得主要list中的那筆資料index
            //var query = this.list_innerObj.Select((item, index) => new { Item = item, Index = index })
            //            .FirstOrDefault(x =>
            //                x.Item.dbColumn == this.lstVwSubItems.Items[e.Index].SubItems[3].Text &&
            //                //x.Item.itemChtName == this.lstVwSubItems.Items[e.Index].SubItems[2].Text &&
            //                x.Item.itemEngName == this.lstVwSubItems.Items[e.Index].SubItems[4].Text &&
            //                x.Item.itemType == this.lstVwSubItems.Items[e.Index].SubItems[5].Text &&
            //                x.Item.index == Convert.ToInt16(this.lstVwSubItems.Items[e.Index].Text));

            //textBoxResults.AppendText($"{JsonConvert.SerializeObject(query)}");

            //if (query == null)
            //{
            //    textBoxResults.Clear();
            //    textBoxResults.AppendText($"query == null{Environment.NewLine}");
            //    return;
            //}

            // 反向
            if (e.CurrentValue == CheckState.Unchecked)
            {
                count++;
                if (this.lstVwSubItems.Items[e.Index].SubItems[1].Text is "")
                    this.lstVwSubItems.Items[e.Index].SubItems[1].Text = count.ToString();          // [1]:順序
                ListViewItem.ListViewSubItem aaa = this.lstVwSubItems.Items[e.Index].SubItems[2];   // [2]:itemChtName


                //textBoxResults.Clear();
                //textBoxResults.AppendText(
                //    //$"{this.lstVwSubItems.Items},{Environment.NewLine}" +
                //    //$"{this.lstVwSubItems.Items[e.Index]},{Environment.NewLine}" +
                //    $"{this.lstVwSubItems.Items[e.Index].SubItems[0].Text},{Environment.NewLine}" +
                //    $"{this.lstVwSubItems.Items[e.Index].SubItems[1].Text},{Environment.NewLine}" +
                //    $"{this.lstVwSubItems.Items[e.Index].SubItems[2].Text},{Environment.NewLine}" +
                //    $"{this.lstVwSubItems.Items[e.Index].SubItems[3].Text},{Environment.NewLine}" +
                //    $"{this.lstVwSubItems.Items[e.Index].SubItems[4].Text},{Environment.NewLine}" +
                //    $"{this.lstVwSubItems.Items[e.Index].SubItems[5].Text},{Environment.NewLine}" +
                //    $"{this.lstVwSubItems.Items[e.Index].SubItems[6].Text},{Environment.NewLine}"
                //    );

                int innerindex = Convert.ToInt16(this.lstVwSubItems.Items[e.Index].SubItems[6].Text);   // [6]:index
                if (this.list_innerObj[innerindex].SubSeq is null)
                    this.list_innerObj[innerindex].SubSeq = count;  // 指定順序給主要objectlist
                this.list_innerObj[innerindex].Checked = true;

                //取得主要objectlist中符合點擊當行得資料
                //var testlist2 = this.list_innerObj.Where(x =>
                //    x.dbColumn == this.lstVwSubItems.Items[e.Index].SubItems[3].Text &&
                //    x.itemChtName == this.lstVwSubItems.Items[e.Index].SubItems[2].Text &&
                //    x.itemEngName == this.lstVwSubItems.Items[e.Index].SubItems[4].Text &&
                //    x.itemType == this.lstVwSubItems.Items[e.Index].SubItems[5].Text &&
                //    x.index == Convert.ToInt16(this.lstVwSubItems.Items[e.Index].Text)).ToList();
                var testlist2 = this.list_innerObj.Where(x => x.innerIndex == innerindex).ToList();
                textBoxResults.AppendText($"{JsonConvert.SerializeObject(testlist2)},{Environment.NewLine}");

                ////取得對應的LOV OBJ
                //var testlist3 = this.list_LovObj.Where(x => x.itemType == lstVwSubItems.Items[e.Index].SubItems[5].Text).ToList();  // [5]:itemType
                //textBoxResults.AppendText($"{JsonConvert.SerializeObject(testlist3)},{Environment.NewLine}");

            }
            else if ((e.CurrentValue == CheckState.Checked))
            {
                count--;
                this.lstVwSubItems.Items[e.Index].SubItems[1].Text = "";
                int innerindex = Convert.ToInt16(this.lstVwSubItems.Items[e.Index].SubItems[6].Text);
                this.list_innerObj[innerindex].SubSeq = null;  // 指定順序給主要objectlist
                this.list_innerObj[innerindex].Checked = false;
            }

            if (list_innerObj.Count > 0)
                dGV_innerObj.DataSource = list_innerObj;

        }

        //private void lstVwSubItems_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //if (this.lstVwSubItems.SelectedItems.Count > 0)
        //    //{

        //    //    ListView.SelectedListViewItemCollection SelectedItems = this.lstVwSubItems.SelectedItems;
        //    //    textBoxResults.AppendText($"{JsonConvert.SerializeObject(this.lstVwSubItems.SelectedItems[0].Index)}");
        //    //    if (this.lstVwSubItems.Items[SelectedItems[0].Index].SubItems[1].Text == "")
        //    //    {
        //    //        count++;
        //    //        this.lstVwSubItems.Items[SelectedItems[0].Index].SubItems[1].Text = count.ToString();
        //    //    }
        //    //    else
        //    //    {
        //    //        count--;
        //    //        this.lstVwSubItems.Items[SelectedItems[0].Index].SubItems[1].Text = "";
        //    //    }


        //    //}


        //}

        /// <summary>
        /// 清空訊息框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_clearMessege_Click(object sender, EventArgs e)
        {
            textBoxResults.Clear();
        }

        /// <summary>
        /// 執行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_run_Click(object sender, EventArgs e)
        {
            textBoxResults.Clear();
            List<string> allResultStr = new List<string>();
            foreach (DataGridViewRow row in this.dGdVwHeaders.Rows)
            {
                // 檢查行的類型，以排除 Header 和其他非資料行
                if (row.IsNewRow) continue;

                // 逐列取得每個儲存格的內容值
                int _dataindex = Convert.ToInt16(row.Cells[0].Value);
                string _dataType = row.Cells[1].Value?.ToString();
                // ... 根據需要取得其他儲存格的值

                // 使用取得的值進行後續處理
                //textBoxResults.AppendText($"Row: " + _dataindex + " | " + _dataType + ","+ Environment.NewLine);

                //-----------------------------------------------------------------------------

                // 使用 Where 方法篩選同類的資料
                // 使用 OrderBy 方法依據指定的元素欄位進行重新排序
                List<innerObj> current_list = this.list_innerObj.Where(x => x.index == _dataindex).OrderBy(obj => obj.SubSeq).ToList().FindAll(e => e.SubSeq != null);

                /*標題部分  無變數不需修改*/
                string _templateModel_grid_header_start = "<<<< Grid >>>>\r\n//------------------------------------------------\r\n\r\n    return [\r\n      {\r\n        headerName: '項目',\r\n        headerValueGetter: this._agService.headerValueGetter,\r\n        field: 'ITEM',\r\n      },\r\n";
                string _templateModel_grid_header_end = "    ];\r\n\r\n//------------------------------------------------\r\n\r\n\r\n\r\n";
                string _templateModel_form_header_start = "<<<< Form >>>>\r\n//------------------------------------------------\r\n\r\n    const controls: FormBase<any>[] = [\r\n";
                string _templateModel_form_header_end = "    ];\r\n\r\n//------------------------------------------------\r\n\r\n\r\n\r\n";

                /*GRID部分*/
                string _templateModel_grid_view_normal = "      {{\r\n        headerName: '{0}',\r\n        headerValueGetter: this._agService.headerValueGetter,\r\n        field: '{1}',\r\n        type: 'text',\r\n        editable: false,\r\n        width: 100,\r\n        suppressSizeToFit: true,\r\n      }},\r\n";
                string _templateModel_grid_view_number = "      {{\r\n        headerName: '{0}',\r\n        headerValueGetter: this._agService.headerValueGetter,\r\n        field: '{1}',\r\n        type: 'number',\r\n        valueFormatter: this._pubFunction.formatNumber.bind(this, 2), //含 千份位＋顯示小數點後幾位\r\n        cellStyle: this._pubFunction.rightTextAlign,\r\n        floatingFilterComponent: 'numberFilterRenderer',\r\n        suppressSizeToFit: true,\r\n      }},\r\n";
                string _templateModel_grid_view_date = "      {{\r\n        headerName: '{0}',\r\n        headerValueGetter: this._agService.headerValueGetter,\r\n        field: '{1}',\r\n        type: 'date',\r\n        editable: false,\r\n        width: 120,\r\n        suppressSizeToFit: true,\r\n        valueFormatter: this._pubFunction.formatDate,\r\n        // valueFormatter: (params) => {{\r\n        //   return this._pubFunction.formatDate({{ value: params.data?.{1} }}, 'yyyy/MM/dd');\r\n        // }},\r\n      }},\r\n";
                string _templateModel_grid_confirm = "      {{\r\n        headerName: '{0}', //勾選\r\n        headerValueGetter: this._agService.headerValueGetter,\r\n        field: '{1}',\r\n        editable: false,\r\n        filter: false,\r\n        sortable: false,\r\n        cellRenderer: 'confirmRenderer',\r\n        width: 50,\r\n        suppressSizeToFit: true,\r\n        cellRendererParams: (params) => {{\r\n          // if (params.data?.ITEM === 0) return {{ disabled: true }};\r\n\r\n          return {{\r\n            checkValueType: EAgGridCheckValueType.booleanType,\r\n            // afterCheckedFunc: (value: boolean) => {{\r\n            //   if (value) {{\r\n            //     //判斷通過，勾選\r\n            //     params.data.{1} = true;\r\n            //   }} else {{\r\n            //     //取消勾選\r\n            //     params.data.{1} = false;\r\n            //   }}\r\n            //   params.node.setData(params.data);\r\n            // }},\r\n          }};\r\n        }},\r\n      }},\r\n";
                string _templateModel_grid_check = "      {{\r\n        headerName: '{0}', \r\n        headerValueGetter: this._agService.headerValueGetter,\r\n        field: '{1}',\r\n        type: ['wrappingHeader'],\r\n        editable: false,\r\n        filter: false,\r\n        sortable: false,\r\n        cellRenderer: 'checkRenderer',\r\n        width: 50,\r\n        suppressSizeToFit: true,\r\n        valueGetter: (params) => {{\r\n          return params.data?.{1} === 'Y';\r\n        }},\r\n        valueSetter: (params) => {{\r\n          params.data.{1} = params.newValue ? 'Y' : null;\r\n          return true;\r\n        }},\r\n        cellRendererParams: (params) => {{\r\n          // if (params.data?.ITEM === 0) return {{ disabled: true }};\r\n          return {{\r\n            checkValueType: EAgGridCheckValueType.stringType,\r\n            onCheckedFunc: (value: string) => {{\r\n              // if (value == 'Y')\r\n              //   params.data.{1} = 'Y';  //勾選\r\n              // else\r\n              //   params.data.{1} = 'N';  //取消勾選\r\n              // params.node.setData(params.data);\r\n            }},\r\n          }};\r\n        }},\r\n      }},\r\n";
                //string _templateModel_grid_select = "      {{\r\n        headerName: '{1}',\r\n        field: '{0}',\r\n        type: 'text',\r\n        width: 100,\r\n        suppressSizeToFit: true,\r\n        valueFormatter: (params) => {{\r\n          return {{\r\n            // PASS: 'PASS',\r\n            // FAIL: 'FAIL',\r\n          }}[params.data.{0}];\r\n        }},\r\n        cellEditor: 'selectEditor',\r\n        cellEditorParams: {{\r\n          control: {{\r\n            multiple: false,\r\n            field: ['{0}'],\r\n            options: of([\r\n              // {{ key: 'PASS', value: 'PASS' }},\r\n              // {{ key: 'FAIL', value: 'FAIL' }},\r\n            ]),\r\n          }},\r\n        }},\r\n        floatingFilterComponent: 'selectFilterRenderer',\r\n        floatingFilterComponentParams: {{\r\n          options: of([\r\n            // {{ key: 'PASS', value: 'PASS' }},\r\n            // {{ key: 'FAIL', value: 'FAIL' }},\r\n          ]),\r\n        }},\r\n      }},\r\n";
                string _templateModel_grid_select = "      {{\r\n        headerName: '{0}',\r\n        headerValueGetter: this._agService.headerValueGetter,\r\n        field: '{1}',\r\n        type: 'text',\r\n        editable: true, //(params) => {{return params.data?.ITEM === 0;}},// 可新增不可修改\r\n        sortable: true,\r\n        width: 100,\r\n        suppressSizeToFit: true,\r\n        valueFormatter: (params) => {{\r\n          const displayValue = params.node?.data?.{1} ?? '';\r\n          return {{ \r\n            // '1': 'Cosmos/KM3代工', '3': '組底加工' \r\n          }}[displayValue] ?? '';\r\n        }},\r\n        // valueSetter: (params) => {{\r\n        //   const optKey = params.newValue?.[0];\r\n        //   params.data.{1} = optKey;\r\n        //   return true;\r\n        // }},\r\n        cellEditor: 'selectRenderer',\r\n        cellEditorParams: {{\r\n          control: {{\r\n            multiple: false,\r\n            field: ['{1}'],\r\n            options: of([\r\n              // {{ key: '1', value: 'Cosmos/KM3代工' }},\r\n              // {{ key: '3', value: '組底加工' }},\r\n            ]),\r\n          }},\r\n        }},\r\n        floatingFilterComponent: 'selectFilterRenderer',\r\n        floatingFilterComponentParams: {{\r\n          options: of([\r\n            // {{ key: '1', value: 'Cosmos/KM3代工' }},\r\n            // {{ key: '3', value: '組底加工' }},\r\n          ]),\r\n        }},\r\n      }},\r\n";

                /*FORM部分*/
                string _templateModel_form_normal = "      new FormTextBox({{\r\n        key: '{1}',\r\n        label: '{0}',\r\n        // labelWidth: 5,\r\n        flex: '20',\r\n        order: 1,\r\n        class: 'pr-1',\r\n        // readonly: true,\r\n      }}),\r\n";  // textAlign: 'right',\r\n
                string _templateModel_form_btn = "      new FormButtonAuthority({{\r\n        showButtons: [\r\n          <IButtons>{{\r\n            key: 'confirm',\r\n            color: 'primary',\r\n            text: '{0}',\r\n            icon: 'done_all',\r\n            visible: true,\r\n            //visibledAsync: this.State.FormRefs.visibleAsync$,\r\n            clickFunction: () => {{\r\n              //this.Service.pushSave();\r\n            }},\r\n          }},\r\n        ],\r\n        flex: '20',\r\n        order: 1,\r\n        class: 'pr-1',\r\n      }}),\r\n";
                string _templateModel_form_radio_btn = "      new FormRadioButton({{\r\n        key: '{0}',\r\n        label: '',\r\n        options: of([\r\n          // {{ key: 'A', value: '中文顯示A' }},\r\n          // {{ key: 'B', value: '中文顯示B' }},\r\n          // {{ key: 'C', value: '中文顯示C' }},\r\n        ]),\r\n        flex: '45',\r\n        // value: 'A',\r\n        inputChangeFunc: (params) => {{\r\n          // this._Service.query();\r\n        }},\r\n      }}),\r\n";
                string _templateModel_form_hidden = "      new FormHidden({\r\n        key: '',\r\n        label: '',\r\n        order: 1,\r\n        flex: '20',\r\n      }),\r\n";
                string _templateModel_form_drop_down_list = "      new FormDropDownList({{\r\n        key: '{0}',\r\n        label: '{1}',\r\n        labelWidth: 10,\r\n        flex: 25,\r\n        class: 'pr-1',\r\n        options: of([\r\n          // {{ key: 'A', value: '顯示文字A' }},\r\n          // {{ key: 'B', value: '顯示文字B' }},\r\n          // {{ key: 'C', value: '顯示文字C' }},\r\n        ]),\r\n        inputChangeFunc: (value, form) => {{\r\n          //this._Service.update();\r\n        }},\r\n      }}),\r\n";
                string _templateModel_form_hidden2 = "      new FormHidden({}),\r\n";


                /*LOV部分*/
                string _templateModel_grid_Lovl = "      {{\r\n        headerName: '{0}',\r\n        headerValueGetter: this._agService.headerValueGetter,\r\n        field: '{1}',\r\n        suppressSizeToFit: true,\r\n        editable: true, //(params) => {{ return params.data.ITEM === 0; // 可新增不可修改 }},\r\n        sortable: true,\r\n        width: 120,\r\n        type: 'text',\r\n        cellEditor: 'lovEditor',\r\n        cellEditorParams: (params) => {{\r\n          return <ILovEditorParams>{{\r\n            apiParams: {{\r\n              // sp前綴\r\n              moduleNo: 模組名稱,\r\n              programNo: '{2}',\r\n              commonApiType: ECommonApiType.CallStoreProcedureDataSet,\r\n            }},\r\n            queryAction: sp名稱, // sp名稱\r\n            //payload: {{}}, // input\r\n            refCursorKeys: [v表名稱Info, v表名稱Count], // output\r\n            colDefs: [\r\n{3}            ],\r\n            keyMapping: {{              {4}\r\n            }},\r\n            checkInput: true,\r\n            onPostChange: (params) => {{\r\n              if (params.isValidInput) //this._Service.ServiceFun(params.value);\r\n            }},\r\n          }};\r\n        }},\r\n      }},\r\n";
                string _templateModel_form_Lovl = "      new FormLov({{\r\n        key: '{0}',\r\n        label: '{1}',\r\n        //labelWidth: '10',\r\n        apiParams: {{\r\n          moduleNo: 模組名稱,\r\n          programNo: '{2}',\r\n          commonApiType: ECommonApiType.CallStoreProcedureDataSet,\r\n        }},\r\n        queryAction: SP名稱, // sp名稱\r\n        refCursorKeys: [v表名稱Info, v表名稱Count],\r\n        colDefs: [\r\n{3}        ],\r\n        keyMapping: {{{4}\r\n        }},\r\n        checkInput: true,\r\n        flex: '25',\r\n        class: 'pr-1',\r\n      }}),\r\n";

                /*其他*/
                string resultStr = string.Empty;
                List<string> list_resultStr = new List<string>();



                if (_dataType == "Grid")
                    resultStr += _templateModel_grid_header_start;
                else if (_dataType == "Form")
                    resultStr += _templateModel_form_header_start;
                else if (_dataType == null || _dataType == "") { }
                else
                    MessageBox.Show("Grid/Form 欄位填寫有誤");

                foreach (innerObj obj in current_list)
                {

                    if ((obj.dbColumn == null || obj.dbColumn == string.Empty) && obj.itemEngName != null)
                        obj.dbColumn = obj.itemEngName.ToUpper();

                    if (obj.dbColumn != null)
                        obj.dbColumn = obj.dbColumn.ToUpper();

                    //textBoxResults.AppendText(String.Format(_templateModel2, obj.itemChtName, obj.dbColumn));
                    if (_dataType == "Grid")
                    {
                        string _templateModel = "";
                        if (obj.itemType == "文字項目" || obj.itemType == "顯示項目"
                            || obj.itemType == "Text Item" || obj.itemType == "Display Item")
                        {
                            _templateModel = Fun_Replace_GridView_String("View", obj, _templateModel_grid_view_number, _templateModel_grid_view_date, _templateModel_grid_view_normal);
                            list_resultStr.Add(String.Format(_templateModel, obj.itemChtName, obj.dbColumn));
                        }
                        else if (obj.itemType == "控制項目")
                        {
                            _templateModel = Fun_Replace_GridView_String("noView", obj, _templateModel_grid_view_number, _templateModel_grid_view_date, _templateModel_grid_view_normal);
                            list_resultStr.Add(String.Format(_templateModel, obj.itemChtName, obj.dbColumn));
                        }
                        else if (obj.itemType == "核取方塊" || obj.itemType == "Check Box")
                            list_resultStr.Add(String.Format(_templateModel_grid_confirm, obj.itemChtName, obj.dbColumn));
                        else if (obj.itemType == "值選擇框")
                            list_resultStr.Add(String.Format(_templateModel_grid_check, obj.itemChtName, obj.dbColumn));
                        else if (obj.itemType.Contains("LOV_"))
                            list_resultStr.Add(String.Format(_templateModel_grid_Lovl, obj.itemChtName, obj.dbColumn, Path.GetFileNameWithoutExtension(filePath), Fun_Lov_Detail_String(obj.itemType, 0), Fun_Lov_Detail_String(obj.itemType, 1)));
                        else if (obj.itemType == "清單項目" || obj.itemType == "List Item")
                            list_resultStr.Add(String.Format(_templateModel_grid_select, obj.itemChtName, obj.dbColumn));
                        else
                            list_resultStr.Add(String.Format(_templateModel_grid_view_normal, obj.itemChtName, obj.dbColumn));
                    }
                    else if (_dataType == "Form")
                    {
                        if (obj.itemType == "文字項目" || obj.itemType == "顯示項目"
                            || obj.itemType == "Text Item" || obj.itemType == "Display Item")
                            list_resultStr.Add(String.Format(_templateModel_form_normal, obj.itemChtName, obj.dbColumn));
                        else if (obj.itemType == "按鈕" || obj.itemType == "Push Button")
                            list_resultStr.Add(String.Format(_templateModel_form_btn, obj.itemChtName));
                        else if (obj.itemType.Contains("LOV_"))
                            list_resultStr.Add(String.Format(_templateModel_form_Lovl, obj.itemChtName, obj.dbColumn, Path.GetFileNameWithoutExtension(filePath), Fun_Lov_Detail_String(obj.itemType, 0), Fun_Lov_Detail_String(obj.itemType, 1)));
                        else if (obj.itemType == "圓鈕群組" || obj.itemType == "Radio Group") //
                            list_resultStr.Add(String.Format(_templateModel_form_radio_btn, obj.itemEngName));
                        else if (obj.itemType == "清單項目" || obj.itemType == "List Item") //
                            list_resultStr.Add(String.Format(_templateModel_form_drop_down_list, obj.itemEngName, obj.itemChtName));
                        else if (obj.itemType == "Form隱藏格式")
                            list_resultStr.Add(_templateModel_form_hidden);
                        else
                            list_resultStr.Add(String.Format(_templateModel_form_normal, obj.itemChtName, obj.dbColumn));
                    }


                }

                foreach (var resultStr2 in list_resultStr)
                    resultStr += resultStr2;

                if (_dataType == "Grid")
                    resultStr += _templateModel_grid_header_end;
                else if (_dataType == "Form")
                    resultStr += _templateModel_form_header_end;
                else if (_dataType == null) { };

                textBoxResults.AppendText(resultStr);
                allResultStr.Add(resultStr);

                //-----------------------------------------------------------------------------
            }

            FileWrite(allResultStr, false, "");
            //開啟copy.txt
            System.Diagnostics.Process.Start("explorer.exe", "temp.txt");

        }


        /// <summary>
        /// GridView腳本替換細節部分
        /// </summary>
        /// <param name="datarow"></param>
        public string Fun_Replace_GridView_String(string _type_flag, innerObj obj, string _view_number, string _view_date, string _view_normal)
        {
            string _templateModel = "";

            obj.itemEngName = obj.itemEngName is not null ? obj.itemEngName : "";

            if (obj.itemEngName.ToLower().Contains("qty") || obj.itemEngName.ToLower().Contains("price")
                || obj.itemEngName.ToLower().Contains("gw") || obj.itemEngName.ToLower().Contains("gw"))
            {
                if (_type_flag == "View")
                    _templateModel = _view_number;
                else
                    _templateModel = _view_number.Replace("      }},\r\n", "        cellEditor: 'textBoxRenderer',\r\n        cellEditorParams: () => ({{\r\n          control: {{\r\n            type: 'number',\r\n            max: '99999',\r\n            min: '0',\r\n            step: '1',\r\n            decimal: 0, //顯示小數點位數\r\n            allowEmpty: true,\r\n          }},\r\n        }}),\r\n      }},\r\n");
            }
            else if (obj.itemEngName.ToLower().Contains("date") || obj.itemEngName.ToLower().Contains("yymm"))
                _templateModel = _view_date;
            else
                _templateModel = _view_normal;

            if (obj.itemChtName == null || obj.itemChtName == string.Empty)
            {
                string _templateModel_grid_noChtName = _templateModel.Replace("headerValueGetter: this._agService.headerValueGetter,", "//headerValueGetter: this._agService.headerValueGetter,");
                _templateModel = _templateModel_grid_noChtName;
            }


            return _templateModel;
        }


        /// <summary>
        /// 載入細節部分
        /// </summary>
        /// <param name="datarow"></param>
        public void Fun_Reload_SubDataGridView(DataGridViewRow datarow)
        {
            if (datarow == null) return;


            lstVwSubItems.Clear();
            this.count = 0; //全域
            //MessageBox.Show(datarow.Cells[0].Value.ToString());
            //MessageBox.Show(e.ToString());
            var testlist2 = this.list_innerObj.Where(x => x.index == Convert.ToInt32(datarow.Cells[0].Value)
                ).ToList();

            //foreach (var resultstr2 in testlist2)
            //    textBoxResults.AppendText($"{JsonConvert.SerializeObject(resultstr2)}");

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            lstVwSubItems.Columns.Add("選取並訂順序", 100, HorizontalAlignment.Left);
            lstVwSubItems.Columns.Add("順序", 100, HorizontalAlignment.Left);

            lstVwSubItems.Columns.Add("itemChtName", 150, HorizontalAlignment.Left);
            lstVwSubItems.Columns.Add("dbColumn", 150, HorizontalAlignment.Left);
            lstVwSubItems.Columns.Add("itemEngName", 150, HorizontalAlignment.Left);
            lstVwSubItems.Columns.Add("itemType", 150, HorizontalAlignment.Center);
            lstVwSubItems.Columns.Add("index", 10, HorizontalAlignment.Left);


            foreach (var resultstr2 in testlist2)
            {
                // Create three items and three sets of subitems for each item.
                ListViewItem item1 = new ListViewItem("區段" + resultstr2.index.ToString().Trim(), 1);
                // Place a check mark next to the item.
                item1.Checked = resultstr2.Checked;
                //if(resultstr2.SubSeq != null) { MessageBox.Show(resultstr2.SubSeq.ToString(),"input"); }
                item1.SubItems.Add(resultstr2.SubSeq.ToString());

                item1.SubItems.Add(resultstr2.itemChtName);
                item1.SubItems.Add(resultstr2.dbColumn);
                item1.SubItems.Add(resultstr2.itemEngName);
                item1.SubItems.Add(resultstr2.itemType);
                item1.SubItems.Add(resultstr2.innerIndex.ToString());
                //item1.SubItems[1].Text =  resultstr2.SubSeq.ToString();
                //item1.SubItems[1].Text = resultstr2.SubSeq.ToString();

                //Add the items to the ListView.
                lstVwSubItems.Items.AddRange(new ListViewItem[] { item1 });
                //if (item1.SubItems[1].Text != "") { MessageBox.Show(item1.SubItems[1].Text,"output"); }
            }
        }

        /// <summary>
        /// 轉換成 LOV Detail字串
        /// </summary>
        /// <param name="indexString">擷取字串</param>
        /// <param name="index">0:colDefs部分 1:keyMapping部分</param>
        /// <returns>LOV Detail字串</returns>
        public string Fun_Lov_Detail_String(string indexString, int index)
        {
            var itemObj = this.list_LovObj.Where(x => x.itemType == indexString).ToList();

            string _template_Lovl = "              {{\r\n                headerName: '{0}',\r\n                headerValueGetter: this._agService.headerValueGetter,\r\n                field: '{1}',\r\n                type: 'text',\r\n              }},\r\n";
            string _template_Lovl_DlpGrid = "          {{\r\n            headerName: '{0}',\r\n            field: '{1}',\r\n            type: EDlpGridColumnType.text,\r\n          }},\r\n";
            string _template_Lov2 = "\r\n              {0}: '{1}',";
            string del_strng = "", map_string = "";

            foreach (var item in itemObj)
            {
                del_strng += String.Format(_template_Lovl, item.itemChtName, item.itemEngName);

                if (item.dbColumn != string.Empty)
                {
                    string[] splitStr = item.dbColumn.Split('.');
                    map_string += String.Format(_template_Lov2, splitStr[1], item.itemEngName);
                }
            }

            return index is 0 ? del_strng : map_string;
        }

        //private void dGdVwHeaders_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        //{
        //    //CellContextMenuStripNeeded　事件处理方法的参数中、「e.ColumnIndex=-1」表示行头、「e.RowIndex=-1」表示列头。

        //    DataGridView dgv = (DataGridView)sender;

        //    if (contextMenu == null)
        //    {
        //        //contextMenu = new ContextMenuStrip();
        //        //toolstrip.Text = "刪除";
        //        //toolstrip.Text = "設定FORM";
        //        //toolstrip.Text = "設定GRID";
        //        //contextMenu.Items.Add(toolstrip);
        //    }

        //    if (dgv[e.ColumnIndex, e.RowIndex].Value is int)
        //    {
        //        //　如果单元格值是整数时
        //        e.ContextMenuStrip = contextMenu;
        //    }


        //}


        //private void toolstrip_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("確定要刪除該筆資料？", "警告!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //    {
        //        //如果按下yes就會執行刪除指令,這邊就看個人發揮囉!!
        //    }
        //}

        //右鍵事件
        private void Item1_Click(object sender, EventArgs e)
        {
            // 設定新的值
            dGdVwHeaders.Rows[tempRowIndex].Cells[tempColumnIndex].Value = "";

        }
        //右鍵事件
        private void Item2_Click(object sender, EventArgs e)
        {
            // 設定新的值
            dGdVwHeaders.Rows[tempRowIndex].Cells[tempColumnIndex].Value = "Form";
        }
        //右鍵事件
        private void Item3_Click(object sender, EventArgs e)
        {
            // 設定新的值
            dGdVwHeaders.Rows[tempRowIndex].Cells[tempColumnIndex].Value = "Grid";
        }

        //取得右鍵cell資訊
        private void dGdVwHeaders_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            tempColumnIndex = e.ColumnIndex;
            tempRowIndex = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                if (!dGdVwHeaders.Rows[tempRowIndex].Selected)
                {
                    dGdVwHeaders.ClearSelection();
                    dGdVwHeaders.Rows[tempRowIndex].Selected = true;
                }
            }

        }

        public void FileWrite(List<string> Text, Boolean append, string Scope)
        {
            TextWriter txt = new StreamWriter(@".\temp.txt", append);

            if (Scope == "Start")
            {
                //Clear
                txt.Write("");
            }
            else
            {
                if (Text.Count > 0)
                {
                    foreach (string t in Text)
                    {
                        txt.WriteLine(t);
                    }
                }
                else
                {
                    txt.WriteLine("");
                }
            }


            txt.Close();
        }

        private void lstVwSubItems_MouseUp(object sender, MouseEventArgs e)
        {
            lvi = this.lstVwSubItems.GetItemAt(e.X, e.Y);
            if (lvi != null)
            {
                string itemType = lvi.SubItems[5].Text;

                if (itemType == "文字項目" || itemType == "顯示項目"
                    || itemType == "Text Item" || itemType == "Display Item"
                    || itemType == "控制項目" || itemType == "顯示項目"
                    )
                {
                    this.comboBox1.Items.Clear();
                    this.comboBox1.Visible = true;
                    this.comboBox1.Items.Add("控制項目");
                    this.comboBox1.Items.Add("顯示項目");
                }
                else if (itemType == "核取方塊" || itemType == "Check Box"
                    || itemType == "值選擇框" || itemType == "核取方塊"
                    )
                {
                    this.comboBox1.Items.Clear();
                    this.comboBox1.Visible = true;
                    this.comboBox1.Items.Add("值選擇框");
                    this.comboBox1.Items.Add("核取方塊");
                }
                else
                {
                    this.comboBox1.Visible = false;
                    return;
                }


                //获取选中行的Bounds 
                Rectangle Rect = lvi.Bounds;
                int LX = lstVwSubItems.Columns[4].Width;
                int RX = lstVwSubItems.Columns[4].Width + lstVwSubItems.Columns[5].Width;
                // if (e.X > RX || e.X < LX)
                //{
                //this.comboBox1.Visible = false;
                //Rect.X = lstVwSubItems.Left + lstVwSubItems.Columns[0].Width + 2;
                Rect.Y = this.lstVwSubItems.Top + 2 + Rect.Y;
                //Rect.Width = lstVwSubItems.Columns[1].Width + 2;


                // Right side of cell is in view.
                Rect.Width = lstVwSubItems.Columns[5].Width + Rect.Left;
                Rect.X = lstVwSubItems.Left +
                    lstVwSubItems.Columns[0].Width +
                    lstVwSubItems.Columns[1].Width +
                    lstVwSubItems.Columns[2].Width +
                    lstVwSubItems.Columns[3].Width +
                    lstVwSubItems.Columns[4].Width + 2;

                this.comboBox1.Bounds = Rect;
                this.comboBox1.Text = lvi.SubItems[5].Text;
                this.comboBox1.Visible = true;
                this.comboBox1.BringToFront();
                this.comboBox1.Focus();
                //}
                // int intColIndex = lvi.SubItems.IndexOf(lvi.GetSubItemAt(e.X, e.Y));
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            // Set text of ListView item to match the ComboBox.
            lvi.SubItems[5].Text = comboBox1.Text;
            this.comboBox1.Visible = false;
        }

        private void comboBox1_MouseLeave(object sender, EventArgs e)
        {
            // Set text of ListView item to match the ComboBox.
            lvi.SubItems[5].Text = comboBox1.Text;
            this.comboBox1.Visible = false;
        }
    }
}