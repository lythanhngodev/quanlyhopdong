using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace quanlyhopdong.quanly
{
    /// <summary>
    /// Summary description for nhapexcelhopdong
    /// </summary>
    public class nhapexcelhopdong : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/")))
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/"));
                string dirFullPath = HttpContext.Current.Server.MapPath("~/Upload/");
                string[] files;
                int numFiles;
                files = System.IO.Directory.GetFiles(dirFullPath);
                numFiles = files.Length;
                numFiles = numFiles + 1;
                string tenFile = "";

                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    string fileName = file.FileName;
                    string fileExtension = file.ContentType;
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Upload/"));
                        FileInfo[] rgFiles = di.GetFiles();
                        foreach (FileInfo fi in rgFiles)
                        {
                            fi.Delete();
                        }
                        fileExtension = Path.GetExtension(fileName);
                        string filesave = HttpContext.Current.Server.MapPath("~/Upload/") + fileName;
                        tenFile = fileName;
                        file.SaveAs(filesave);
                    }
                }
                if (File.Exists(HttpContext.Current.Server.MapPath("~/Upload/" + tenFile)))
                {
                    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["StringConnect"].ToString());
                    sqlConn.Open();
                    var wb = new XLWorkbook(HttpContext.Current.Server.MapPath("~/Upload/" + tenFile));
                    var ws = wb.Worksheet(1);
                    int index = 2; // bắt đầu lấy từ dòng 2
                    int kt = 0;
                    // xoá dữ liệu tạm trong bảng hợp đồng temp
                    (new SqlCommand("truncate table tblHopDongTemp", sqlConn)).ExecuteNonQuery();
                    for (int i = 0; i < 20000; i++)
                    {
                        object[] cot = new object[30];
                        if (
                               string.IsNullOrEmpty(ws.Cell(index, "A").Value.ToString()) 
                            && string.IsNullOrEmpty(ws.Cell(index, "B").Value.ToString()) 
                            && string.IsNullOrEmpty(ws.Cell(index, "C").Value.ToString()) 
                            && string.IsNullOrEmpty(ws.Cell(index, "D").Value.ToString())
                            )
                        {
                            break;
                        }
                        // bỏ "F", "G", "H", 
                        string[] chu = { "A", "B", "C", "D", "E", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB" };
                        int p = 0;
                        for (int z = 0; z < 28; z++)
                        {
                            if (z==5||z==6||z==7)
                                continue;
                            else
                            {
                                cot[p] = ws.Cell(index, chu[p]).Value.ToString();
                                p++;
                            }
                        }
                        // đem dữ liệu vào hợp đồng temp;
                        string strSQL = @"INSERT INTO tblHopDongTemp (hoTen, soHopDong, toLam, ngayVaoLam, ngayKy, ngayKetThucHD, luongCoBan, soBHXH, ngaySinh, gioiTinh, soCMND, ngayCap, noiCap, nguyenQuan, danToc, tonGiao, chucDanh, trinhDoVanHoa, trinhTrangGiaDinh, diaChiThuongTruSHK, diaChiThuongTruCMND, soSHK, tenChuHoSHK, quanHeChuHoSHK, ghiChu, ngayNhapLieu, nhapExcel) 
                                        VALUES 
                                        (@hoTen, @soHopDong, @toLam, @ngayVaoLam, @ngayKy, @ngayKetThucHD, @luongCoBan, @soBHXH, @ngaySinh, @gioiTinh, @soCMND, @ngayCap, @noiCap, @nguyenQuan, @danToc, @tonGiao, @chucDanh, @trinhDoVanHoa, @trinhTrangGiaDinh, @diaChiThuongTruSHK, @diaChiThuongTruCMND, @soSHK, @tenChuHoSHK, @quanHeChuHoSHK, @ghiChu, SYSDATETIME(), 1 )";
                        SqlCommand sqlCmd = new SqlCommand(strSQL, sqlConn);
                        sqlCmd.Parameters.AddWithValue("@hoTen", cot[0].ToString()); // A
                        sqlCmd.Parameters.AddWithValue("@soHopDong", cot[1].ToString()); // B
                        sqlCmd.Parameters.AddWithValue("@toLam", cot[2].ToString()); // C
                        sqlCmd.Parameters.AddWithValue("@ngayVaoLam", cot[3].ToString().Split(' ')[0]); // D

                        sqlCmd.Parameters.AddWithValue("@ngayKy", cot[4].ToString().Split(' ')[0]); // E

                        sqlCmd.Parameters.AddWithValue("@ngayKetThucHD", cot[5].ToString().Split(' ')[0]); // I
                        sqlCmd.Parameters.AddWithValue("@luongCoBan", Convert.ToDecimal(cot[6].ToString().Replace(",","").Replace(".",""))); // J
                        sqlCmd.Parameters.AddWithValue("@soBHXH", cot[7].ToString()); // K
                        sqlCmd.Parameters.AddWithValue("@ngaySinh", cot[8].ToString().Split(' ')[0]); // L
                        sqlCmd.Parameters.AddWithValue("@gioiTinh", cot[9].ToString()); // M
                        sqlCmd.Parameters.AddWithValue("@soCMND", cot[10].ToString()); // N
                        sqlCmd.Parameters.AddWithValue("@ngayCap", cot[11].ToString().Split(' ')[0]); // O
                        sqlCmd.Parameters.AddWithValue("@noiCap", cot[12].ToString());  // P
                        sqlCmd.Parameters.AddWithValue("@nguyenQuan", cot[13].ToString());  // Q
                        sqlCmd.Parameters.AddWithValue("@danToc", cot[14].ToString());  // R
                        sqlCmd.Parameters.AddWithValue("@tonGiao", cot[15].ToString()); // S
                        sqlCmd.Parameters.AddWithValue("@chucDanh", cot[16].ToString()); // T
                        sqlCmd.Parameters.AddWithValue("@trinhDoVanHoa", cot[17].ToString()); // U
                        sqlCmd.Parameters.AddWithValue("@trinhTrangGiaDinh", cot[18].ToString());  // V
                        sqlCmd.Parameters.AddWithValue("@diaChiThuongTruSHK", cot[19].ToString()); // W
                        sqlCmd.Parameters.AddWithValue("@diaChiThuongTruCMND", cot[20].ToString()); // X
                        sqlCmd.Parameters.AddWithValue("@soSHK", cot[21].ToString()); // Y
                        sqlCmd.Parameters.AddWithValue("@tenChuHoSHK", cot[22].ToString()); // Z
                        sqlCmd.Parameters.AddWithValue("@quanHeChuHoSHK", cot[23].ToString());  // AA
                        sqlCmd.Parameters.AddWithValue("@ghiChu", cot[24].ToString()); // AB
                        if (sqlCmd.ExecuteNonQuery()>0)
                        {
                            kt++;
                        }
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}