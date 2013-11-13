using System;
using System.IO;
using System.Drawing;
using System.Data;
using System.Text;
using Mono.Data.Sqlite;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Mono.Data.Sqlcipher;

namespace CreateDatabaseWithAdoNet
{
    public partial class CreateDatabaseWithAdoNetViewController : UIViewController
    {
        UIButton _btnCreateDatabase;
        UIButton _btnCreateCipherDB;
        UIButton _btnRead;
        UITextView _txtView;
        
        public CreateDatabaseWithAdoNetViewController() : base ("CreateDatabaseWithAdoNetViewController", null)
        {
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // 建立所需的控制項
            _btnCreateDatabase = UIButton.FromType(UIButtonType.RoundedRect);
            _btnCreateDatabase.Frame = new RectangleF(10, 10, 80, 50);
            _btnCreateDatabase.SetTitle("建立数据库", UIControlState.Normal);

            _btnCreateCipherDB = UIButton.FromType(UIButtonType.RoundedRect);
            _btnCreateCipherDB.Frame = new RectangleF(100, 10, 115, 50);
            _btnCreateCipherDB.SetTitle("建立加密数据库", UIControlState.Normal);

            _btnRead = UIButton.FromType(UIButtonType.RoundedRect);
            _btnRead.Frame = new RectangleF(225, 10, 80, 50);
            _btnRead.SetTitle("读取", UIControlState.Normal);
            

            _txtView = new UITextView(new RectangleF(10, 90, 300, 350));
            _txtView.Editable = false;
            _txtView.ScrollEnabled = true;
            
            _btnCreateDatabase.TouchUpInside += HandleTouchUpInside;
            _btnCreateCipherDB.TouchUpInside += _btnCreateCipherDB_TouchUpInside;
            _btnRead.TouchUpInside += _btnRead_TouchUpInside;
            Add(_btnCreateDatabase);
            Add(_btnCreateCipherDB);
            Add(_btnRead);
            Add(_txtView);
        }


        /// <summary>
        /// 建立資料庫事件處理
        /// </summary>
        void HandleTouchUpInside(object sender, EventArgs e)
        {
            // 建立資料庫
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var pathToDatabase = Path.Combine(documents, "normal.db");
            Mono.Data.Sqlite.SqliteConnection.CreateFile(pathToDatabase);

            var msg = "数据库路径: " + pathToDatabase;
            
            //建立Table
            var connectionString = String.Format("Data Source={0};Version=3;", pathToDatabase);
            using (var conn = new Mono.Data.Sqlite.SqliteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE People (PersonID INTEGER PRIMARY KEY AUTOINCREMENT , FirstName ntext, LastName ntext)";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            //新增資料
            using (var conn = new Mono.Data.Sqlite.SqliteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Insert into People(FirstName,LastName) Values('Terry','Lin') ;";
                    cmd.CommandText += "Insert into People(FirstName,LastName) Values('Ben','Lu') ";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
                
            // 訊息輸出並停用Button
            _txtView.Text = msg;
            _btnCreateDatabase.Enabled = false;         
        }

        /// <summary>
        /// 建立加密資料庫事件處理
        /// </summary>
        void _btnCreateCipherDB_TouchUpInside(object sender, EventArgs e)
        {
            // 建立資料庫
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var pathToDatabase = Path.Combine(documents, "cipher.db");
            Mono.Data.Sqlcipher.SqliteConnection.CreateFile(pathToDatabase);

            var msg = "数据库名称: " + pathToDatabase;

            //建立Table
            var connectionString = String.Format("Data Source={0};Version=3;", pathToDatabase);
            
            using (var conn = new Mono.Data.Sqlcipher.SqliteConnection(connectionString))
            {
                conn.SetPassword("test");
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE People (PersonID INTEGER PRIMARY KEY AUTOINCREMENT , FirstName ntext, LastName ntext)";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            //新增資料
            using (var conn = new Mono.Data.Sqlcipher.SqliteConnection(connectionString ))
            {
                conn.SetPassword("test");
                conn.Open();
                using (var cmd = conn.CreateCommand())
                { 
                    cmd.CommandText = "Insert into People(FirstName,LastName) Values('Terry','Lin') ;";
                    cmd.CommandText += "Insert into People(FirstName,LastName) Values('Ben','Lu') ";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            // 訊息輸出並停用Button
            _txtView.Text = msg;
            _btnCreateCipherDB.Enabled = false;   
        }

        //讀取加密資料庫
        void _btnRead_TouchUpInside(object sender, EventArgs e)
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var pathToDatabase = Path.Combine(documents, "cipher.db");
            var connectionString = String.Format("Data Source={0};Version=3;", pathToDatabase);
            
            using (var conn = new Mono.Data.Sqlcipher.SqliteConnection(connectionString))
            {
                conn.SetPassword("test");
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Select * from People";
                    cmd.CommandType = CommandType.Text;
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        this._txtView.Text += string.Format("First Name:{0} Last Name:{1}\r\n", reader[1].ToString(), reader[2].ToString());
                    }
                    reader.Close();
                }
            }
        }
        
        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
            
            ReleaseDesignerOutlets();
        }
        
        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            // Return true for supported orientations
            return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
        }
    }
}

