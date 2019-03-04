using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace quanlyhopdong.quanly
{
    public partial class quanlyhopdong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1_Rebind(sender, e);
                Grid2_Rebind(sender, e);
                Grid3_Rebind(sender, e);
                // xoá rỗng bảng hợp đồng tạm
                (new SqlFunction()).ExeCuteNonQuery("truncate table tblHopDongTemp");

            }
        }

        protected void Grid1_Rebind(object sender, EventArgs e)
        {
            SqlFunction _sqlfun = new SqlFunction();
            DataTable dt = _sqlfun.GetData(@"
                                            select 
                                            thaoTac= ('<a href=""#"" onclick=""suaDuLieu(''' + CONVERT(NVARCHAR(50),sttHD) + N''')"" class=""btn btn-info btn-xs""   style=""margin-right: 2px;""> <i class=""fa fa-pencil""></i></a><a href=""#"" class=""btn btn-danger btn-xs"" onclick=""xoaDuLieu(''' + CONVERT(NVARCHAR(50),sttHD) + N''')""   style=""margin-right: 2px;""> <i class=""fa fa-trash""></i></a>')
                                            ,*
                                            FROM tblHopDong"
                                            );
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

        [WebMethod(EnableSession = true)]
        public static bool KiemTraNhapExcelHopDong()
        {
            SqlFunction sqlFun = new SqlFunction();
            if (sqlFun.CheckHasRecord("SELECT * FROM tblHopDongTemp"))
            {
                string sql = @"
                            INSERT INTO tblHopDong (hoTen, soHopDong, toLam, ngayVaoLam, ngayKy, ngayKetThucHD, luongCoBan, soBHXH, ngaySinh, gioiTinh, soCMND, ngayCap, noiCap, nguyenQuan, danToc, tonGiao, chucDanh, trinhDoVanHoa, trinhTrangGiaDinh, diaChiThuongTruSHK, diaChiThuongTruCMND, soSHK, tenChuHoSHK, quanHeChuHoSHK, ghiChu, ngayNhapLieu, nhapExcel)
                            SELECT hoTen, soHopDong, toLam, ngayVaoLam, ngayKy, ngayKetThucHD, luongCoBan, soBHXH, ngaySinh, gioiTinh, soCMND, ngayCap, noiCap, nguyenQuan, danToc, tonGiao, chucDanh, trinhDoVanHoa, trinhTrangGiaDinh, diaChiThuongTruSHK, diaChiThuongTruCMND, soSHK, tenChuHoSHK, quanHeChuHoSHK, ghiChu, ngayNhapLieu, nhapExcel
                            FROM tblHopDongTemp
                            ";
                if (sqlFun.ExeCuteNonQuery(sql)) return true;
            }
            return false;
        }
        [WebMethod()]
        public static bool XoaHD(string sttHD)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["StringConnect"].ToString()))
                {
                    sqlConn.Open();
                    string strSQL = "DELETE FROM tblHopDong WHERE sttHD = @sttHD";
                    SqlCommand sqlCmd = new SqlCommand(strSQL, sqlConn);
                    sqlCmd.Parameters.AddWithValue("@sttHD", sttHD);
                    if (sqlCmd.ExecuteNonQuery() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        protected void Grid2_Rebind(object sender, EventArgs e)
        {
            SqlFunction _sqlfun = new SqlFunction();
            DataTable dt = _sqlfun.GetData(@"
                                            select 
                                            *
                                            FROM tblHopDongTemp"
                                            );
            Grid2.DataSource = dt;
            Grid2.DataBind();
        }
        protected void Grid3_Rebind(object sender, EventArgs e)
        {
            SqlFunction _sqlfun = new SqlFunction();
            DataTable dt = _sqlfun.GetData(@"
                                            SELECT 
                                            *
                                            FROM tblCV cv
                                            WHERE cv.soCMND not in (SELECT soCMND FROM tblHopDong)
	                                            Order by sttCV desc"
                                            );
            Grid3.DataSource = dt;
            Grid3.DataBind();
        }
        [WebMethod()]
        public static string LayThangNamNhapHopDong()
        {
            DataTable dt = (new SqlFunction()).GetData("select distinct CONVERT(VARCHAR(7), ngayNhapLieu, 102) AS ngayNhapLieu from tblHopDong");
            return JsonConvert.SerializeObject(dt, Formatting.Indented);
        }
        [WebMethod()]
        public static string NhapTuCVDaChon(string[] sttCV)
        {
            if (sttCV==null)
            {
                return "Không có CV nào được chọn";
            }
            SqlFunction sqlFun = new SqlFunction();
            int thanhcong = 0;
            for (int i = 0; i < sttCV.Length; i++)
            {
                string sql = @"
                                INSERT INTO tblHopDong (hoTen, toLam, ngayVaoLam, luongCoBan, ngaySinh, gioiTinh, soCMND, ngayCap, noiCap, nguyenQuan, danToc, tonGiao, chucDanh, trinhDoVanHoa, trinhTrangGiaDinh, diaChiThuongTruSHK, diaChiThuongTruCMND, soSHK, tenChuHoSHK, quanHeChuHoSHK, ngayNhapLieu)
                                SELECT hoTen, toLam, ngayVaoLam, luongCoBan, ngaySinh, gioiTinh, soCMND, ngayCap, noiCap, nguyenQuan, danToc, tonGiao, chucDanh, trinhDoVanHoa, trinhTrangGiaDinh, diaChiThuongTruSHK, diaChiThuongTruCMND, soSHK, tenChuHoSHK, quanHeChuHoSHK, SYSDATETIME() 
                                FROM tblCV cv WHERE cv.sttCV = '"+ sttCV[i] +@"'
                             ";
                if (sqlFun.ExeCuteNonQuery(sql))
                {
                    thanhcong++;
                }
            }
            return "Đã nhập thành công "+thanhcong+"/"+sttCV.Length+" từ CV";
        }
        [WebMethod()]
        public static string XuatWordHD(string thoigian)
        {
            try
            {
                string fileName = "HD_" + (DateTime.Now.ToString("ddMMyyyyHHmmss")) + ".docx";
                string fileMau = HttpContext.Current.Server.MapPath("~/FileMau/MauHopDong.docx"); // file word mẫu
                string fileKQ = HttpContext.Current.Server.MapPath("~/Word/" + fileName); // file khi xuất
                string url = HttpContext.Current.Server.MapPath("~/Word/");
                if (!System.IO.Directory.Exists(url))
                {
                    System.IO.Directory.CreateDirectory(url);
                }
                DirectoryInfo di = new DirectoryInfo(url);
                FileInfo[] rgFiles = di.GetFiles();
                foreach (FileInfo fi in rgFiles)
                {
                    fi.Delete();
                }

                string sql = @"select distinct * from tblHopDong ";
                string where = @"";

                var application = new Microsoft.Office.Interop.Word.Application();
                var document = new Microsoft.Office.Interop.Word.Document();

                document = application.Documents.Add(Template: fileMau);

                if (string.IsNullOrEmpty(thoigian))
                {
                    return "KhongThanhCong";
                }
                else
                {
                    // có thể lỗi ngay chổ này do lấy ngày nhập liệu ra làm điều kiện
                    where = "where month(ngayNhapLieu) = '"+thoigian.Split(',')[1]+ "' and year(ngayNhapLieu) = '" + thoigian.Split(',')[0] + "'";
                }
                DataTable dtHopDong = (new SqlFunction()).GetData(sql + where);
                if (dtHopDong.Rows.Count > 0)
                {
                    for (int i = 0; i < dtHopDong.Rows.Count; i++)
                    {
                        fileName = "HD_" + (DateTime.Now.ToString("ddMMyyyyHHmmss_")) + i.ToString() + ".docx";
                        fileKQ = HttpContext.Current.Server.MapPath("~/Word/" + fileName);
                        File.Copy(fileMau, fileKQ, true);
                        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(fileKQ, true))
                        {
                            MainDocumentPart mainPart = wordDoc.MainDocumentPart;
                            DocumentFormat.OpenXml.Wordprocessing.Body body = wordDoc.MainDocumentPart.Document.Body;
                            //repalce hoTen
                            var paras = body.Elements();

                            foreach (var text in body.Descendants<Text>())
                            {
                                if (text.Text.Contains("soHopDong"))
                                {
                                    text.Text = text.Text.Replace("soHopDong", dtHopDong.Rows[i]["soHopDong"].ToString().ToUpper());
                                }

                                if (text.Text.Contains("_ngay"))
                                {
                                    text.Text = text.Text.Replace("_ngay", String.Format("{0:dd}", Convert.ToDateTime(dtHopDong.Rows[i]["ngayKy"].ToString())));
                                }

                                if (text.Text.Contains("_thang"))
                                {
                                    text.Text = text.Text.Replace("_thang", String.Format("{0:MM}", Convert.ToDateTime(dtHopDong.Rows[i]["ngayKy"].ToString())));
                                }

                                if (text.Text.Contains("_nam"))
                                {
                                    text.Text = text.Text.Replace("_nam", String.Format("{0:yyyy}", Convert.ToDateTime(dtHopDong.Rows[i]["ngayKy"].ToString())));
                                }

                                if (text.Text.Contains("hoTen"))
                                {
                                    text.Text = text.Text.Replace("hoTen", dtHopDong.Rows[i]["hoTen"].ToString().ToUpper());
                                }

                                if (text.Text.Contains("soCMND"))
                                {
                                    text.Text = text.Text.Replace("soCMND", dtHopDong.Rows[i]["soCMND"].ToString());
                                }

                                if (text.Text.Contains("ngayCap"))
                                {
                                    text.Text = text.Text.Replace("ngayCap", dtHopDong.Rows[i]["ngayCap"].ToString());
                                }

                                if (text.Text.Contains("noiCap"))
                                {
                                    text.Text = text.Text.Replace("noiCap", dtHopDong.Rows[i]["noiCap"].ToString());
                                }

                                if (text.Text.Contains("ngayKy"))
                                {
                                    text.Text = text.Text.Replace("ngayKy", dtHopDong.Rows[i]["ngayKy"].ToString());
                                }

                                if (text.Text.Contains("ngayKetThucHD"))
                                {
                                    text.Text = text.Text.Replace("ngayKetThucHD", dtHopDong.Rows[i]["ngayKetThucHD"].ToString());
                                }

                                if (text.Text.Contains("luongCoBan"))
                                {
                                    text.Text = text.Text.Replace("luongCoBan", string.Format(dtHopDong.Rows[i]["luongCoBan"].ToString(), "{0: n0}"));
                                }
                            }
                            wordDoc.MainDocumentPart.Document.Save();
                            wordDoc.Close();
                        }
                    }
                }
                string[] allWordDocuments = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Word/"), "*.docx", SearchOption.TopDirectoryOnly); //Or if you want only SearchOptions.TopDirectoryOnly
                string tenFileGop = "HD_" + (DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")) + ".docx";
                string outputPath = HttpContext.Current.Server.MapPath("~/Word/") + tenFileGop;
                // copy template mẫu
                MsWord.defaultWordDocumentTemplate = HttpContext.Current.Server.MapPath("~/FileMau/MauHopDongEmpty.docx");
                // gộp các file word lại với nhau
                MsWord.Merge(allWordDocuments, outputPath, true);
                try
                {

                }
                catch (Exception)
                {

                    throw;
                }
                return "/Word/"+tenFileGop;
            }
            catch (Exception ex)
            {
                return "KhongThanhCong";
            }
        }
    }
}