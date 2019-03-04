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

namespace quanlyhopdong.quanly
{
    public partial class quanlycv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1_Rebind(sender, e);
            }
        }

        protected void Grid1_Rebind(object sender, EventArgs e)
        {
            SqlFunction _sqlfun = new SqlFunction();
            DataTable dt = _sqlfun.GetData(@"
                                            select 
                                            thaoTac= ('<a href=""#"" onclick=""suaDuLieu(''' + CONVERT(NVARCHAR(50),sttCV) + N''')"" class=""btn btn-info btn-xs""   style=""margin-right: 2px;""> <i class=""fa fa-pencil""></i></a><a href=""#"" class=""btn btn-danger btn-xs"" onclick=""xoaDuLieu(''' + CONVERT(NVARCHAR(50),sttCV) + N''')""   style=""margin-right: 2px;""> <i class=""fa fa-trash""></i></a>')
                                            ,*
                                            FROM tblCV order by sttCV desc"
                                            );
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
        [WebMethod()]
        public static string ThemCV(object saveData)
        {
            try
            {
                object[] _arrayT = Array.ConvertAll((object[])saveData, s => (object)s);
                //string sqlQuery_ = @"select maTrangppr from tblTrang where maTrangppr=N'" + _arrayT[0].ToString() + "'";
                //SqlFunction sqlfun = new SqlFunction();

                //if (sqlfun.CheckHasRecord(sqlQuery_)) return "TonTai";

                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["StringConnect"].ToString()))
                {
                    sqlConn.Open();
                    string strSQL = @"INSERT INTO tblCV (maCC, hoTen, toLam, ngayVaoLam, thamNien, soDienThoai, nguoiGioiThieu, ngaySinh, gioiTinh, soCMND, ngayCap, noiCap, nguyenQuan, danToc, tonGiao, chucDanh, trinhDoVanHoa, trinhTrangGiaDinh, diaChiThuongTruSHK, soSHK, tenChuHoSHK, quanHeChuHoSHK, ghiChu, phanXuong, ngayNhapLieu, diaChiThuongTruCMND) VALUES (@maCC, @hoTen, @toLam, @ngayVaoLam, @thamNien, @soDienThoai, @nguoiGioiThieu, @ngaySinh, @gioiTinh, @soCMND, @ngayCap, @noiCap, @nguyenQuan, @danToc, @tonGiao, @chucDanh, @trinhDoVanHoa, @trinhTrangGiaDinh, @diaChiThuongTruSHK, @soSHK, @tenChuHoSHK, @quanHeChuHoSHK, @ghiChu, @phanXuong, SYSDATETIME(), @diaChiThuongTruCMND)";
                    SqlCommand sqlCmd = new SqlCommand(strSQL, sqlConn);
                    sqlCmd.Parameters.AddWithValue("@maCC", _arrayT[0].ToString());
                    sqlCmd.Parameters.AddWithValue("@hoTen", _arrayT[1].ToString());
                    sqlCmd.Parameters.AddWithValue("@toLam", _arrayT[2].ToString());
                    
                    if (string.IsNullOrEmpty(_arrayT[3].ToString()))
                        sqlCmd.Parameters.AddWithValue("@ngayVaoLam", DBNull.Value);
                    else
                        sqlCmd.Parameters.AddWithValue("@ngayVaoLam", _arrayT[3].ToString());

                    sqlCmd.Parameters.AddWithValue("@thamNien", _arrayT[4].ToString()); 
                    sqlCmd.Parameters.AddWithValue("@soDienThoai", _arrayT[5].ToString());
                    sqlCmd.Parameters.AddWithValue("@nguoiGioiThieu", _arrayT[6].ToString());

                    if (string.IsNullOrEmpty(_arrayT[7].ToString()))
                        sqlCmd.Parameters.AddWithValue("@ngaySinh", DBNull.Value);
                    else
                        sqlCmd.Parameters.AddWithValue("@ngaySinh", _arrayT[7].ToString());

                    sqlCmd.Parameters.AddWithValue("@gioiTinh", _arrayT[8].ToString());
                    sqlCmd.Parameters.AddWithValue("@soCMND", _arrayT[9].ToString());

                    if (string.IsNullOrEmpty(_arrayT[10].ToString()))
                        sqlCmd.Parameters.AddWithValue("@ngayCap", DBNull.Value);
                    else
                        sqlCmd.Parameters.AddWithValue("@ngayCap", _arrayT[10].ToString());

                    sqlCmd.Parameters.AddWithValue("@noiCap", _arrayT[11].ToString());
                    sqlCmd.Parameters.AddWithValue("@nguyenQuan", _arrayT[12].ToString());
                    sqlCmd.Parameters.AddWithValue("@danToc", _arrayT[13].ToString());
                    sqlCmd.Parameters.AddWithValue("@tonGiao", _arrayT[14].ToString());
                    sqlCmd.Parameters.AddWithValue("@chucDanh", _arrayT[15].ToString());
                    sqlCmd.Parameters.AddWithValue("@trinhDoVanHoa", _arrayT[16].ToString());
                    sqlCmd.Parameters.AddWithValue("@trinhTrangGiaDinh", _arrayT[17].ToString());
                    sqlCmd.Parameters.AddWithValue("@diaChiThuongTruSHK", _arrayT[18].ToString());
                    sqlCmd.Parameters.AddWithValue("@diaChiThuongTruCMND", _arrayT[25].ToString());
                    sqlCmd.Parameters.AddWithValue("@soSHK", _arrayT[19].ToString());
                    sqlCmd.Parameters.AddWithValue("@tenChuHoSHK", _arrayT[20].ToString());
                    sqlCmd.Parameters.AddWithValue("@quanHeChuHoSHK", _arrayT[21].ToString());
                    sqlCmd.Parameters.AddWithValue("@ghiChu", _arrayT[22].ToString());
                    sqlCmd.Parameters.AddWithValue("@phanXuong", _arrayT[23].ToString());

                    if (sqlCmd.ExecuteNonQuery() > 0)
                        return "ThanhCong";
                    else
                        return "Thêm mới bài viết không thành công";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [WebMethod()]
        public static bool SuaCV(object saveData)
        {
            try
            {
                object[] _arrayT = Array.ConvertAll((object[])saveData, s => (object)s);
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["StringConnect"].ToString()))
                {
                    sqlConn.Open();
                    string strSQL = @"
                            UPDATE tblCV
                            SET
                             maCC = @maCC
                            , hoTen = @hoTen
                            , toLam = @toLam
                            , ngayVaoLam = @ngayVaoLam
                            , thamNien = @thamNien
                            , soDienThoai = @soDienThoai
                            , nguoiGioiThieu = @nguoiGioiThieu
                            , ngaySinh = @ngaySinh
                            , gioiTinh = @gioiTinh
                            , soCMND = @soCMND
                            , ngayCap = @ngayCap
                            , noiCap = @noiCap
                            , nguyenQuan = @nguyenQuan
                            , danToc = @danToc
                            , tonGiao = @tonGiao
                            , chucDanh = @chucDanh
                            , trinhDoVanHoa = @trinhDoVanHoa
                            , trinhTrangGiaDinh = @trinhTrangGiaDinh
                            , diaChiThuongTruSHK = @diaChiThuongTruSHK
                            , soSHK = @soSHK
                            , tenChuHoSHK = @tenChuHoSHK
                            , quanHeChuHoSHK = @quanHeChuHoSHK
                            , ghiChu = @ghiChu
                            , phanXuong = @phanXuong , diaChiThuongTruCMND = @diaChiThuongTruCMND
                            WHERE sttCV = @sttCV
                                    ";
                    SqlCommand sqlCmd = new SqlCommand(strSQL, sqlConn);
                    sqlCmd.Parameters.AddWithValue("@sttCV", _arrayT[24].ToString());
                    sqlCmd.Parameters.AddWithValue("@maCC", _arrayT[0].ToString());
                    sqlCmd.Parameters.AddWithValue("@hoTen", _arrayT[1].ToString());
                    sqlCmd.Parameters.AddWithValue("@toLam", _arrayT[2].ToString());

                    if (string.IsNullOrEmpty(_arrayT[3].ToString()))
                        sqlCmd.Parameters.AddWithValue("@ngayVaoLam", DBNull.Value);
                    else
                        sqlCmd.Parameters.AddWithValue("@ngayVaoLam", _arrayT[3].ToString());

                    sqlCmd.Parameters.AddWithValue("@thamNien", _arrayT[4].ToString());
                    sqlCmd.Parameters.AddWithValue("@soDienThoai", _arrayT[5].ToString());
                    sqlCmd.Parameters.AddWithValue("@nguoiGioiThieu", _arrayT[6].ToString());

                    if (string.IsNullOrEmpty(_arrayT[7].ToString()))
                        sqlCmd.Parameters.AddWithValue("@ngaySinh", DBNull.Value);
                    else
                        sqlCmd.Parameters.AddWithValue("@ngaySinh", _arrayT[7].ToString());

                    sqlCmd.Parameters.AddWithValue("@gioiTinh", _arrayT[8].ToString());
                    sqlCmd.Parameters.AddWithValue("@soCMND", _arrayT[9].ToString());

                    if (string.IsNullOrEmpty(_arrayT[10].ToString()))
                        sqlCmd.Parameters.AddWithValue("@ngayCap", DBNull.Value);
                    else
                        sqlCmd.Parameters.AddWithValue("@ngayCap", _arrayT[10].ToString());

                    sqlCmd.Parameters.AddWithValue("@noiCap", _arrayT[11].ToString());
                    sqlCmd.Parameters.AddWithValue("@nguyenQuan", _arrayT[12].ToString());
                    sqlCmd.Parameters.AddWithValue("@danToc", _arrayT[13].ToString());
                    sqlCmd.Parameters.AddWithValue("@tonGiao", _arrayT[14].ToString());
                    sqlCmd.Parameters.AddWithValue("@chucDanh", _arrayT[15].ToString());
                    sqlCmd.Parameters.AddWithValue("@trinhDoVanHoa", _arrayT[16].ToString());
                    sqlCmd.Parameters.AddWithValue("@trinhTrangGiaDinh", _arrayT[17].ToString());
                    sqlCmd.Parameters.AddWithValue("@diaChiThuongTruSHK", _arrayT[18].ToString());
                    sqlCmd.Parameters.AddWithValue("@soSHK", _arrayT[19].ToString());
                    sqlCmd.Parameters.AddWithValue("@tenChuHoSHK", _arrayT[20].ToString());
                    sqlCmd.Parameters.AddWithValue("@quanHeChuHoSHK", _arrayT[21].ToString());
                    sqlCmd.Parameters.AddWithValue("@ghiChu", _arrayT[22].ToString());
                    sqlCmd.Parameters.AddWithValue("@phanXuong", _arrayT[23].ToString());
                    sqlCmd.Parameters.AddWithValue("@diaChiThuongTruCMND", _arrayT[25].ToString());
                    if (sqlCmd.ExecuteNonQuery() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [WebMethod()]
        public static bool XoaCV(string sttCV)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["StringConnect"].ToString()))
                {
                    sqlConn.Open();
                    string strSQL = "DELETE FROM tblCV WHERE sttCV = @sttCV";
                    SqlCommand sqlCmd = new SqlCommand(strSQL, sqlConn);
                    sqlCmd.Parameters.AddWithValue("@sttCV", sttCV);
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

        [WebMethod()]
        public static string LayDuLieuTheoID(string cauTruyVan)
        {
            try
            {
                SqlFunction sqlFun = new SqlFunction();
                string strSQL = "SELECT * FROM dbo.tblCV where sttCV = N'" + cauTruyVan + "'";
                DataTable dt = sqlFun.GetData(strSQL);
                return JsonConvert.SerializeObject(dt, Formatting.Indented); ;
            }
            catch
            {
                return null;
            }
        }
        
        [WebMethod()]
        public static string LayTuDienNguyenQuan(string key)
        {
            try
            {
                SqlFunction sqlFun = new SqlFunction();
                string strSQL = @"
                                SELECT DISTINCT TOP 10 xaHuyenTinh.nguyenQuan FROM (select nguyenQuan FROM dbo.tblTuDien where nguyenQuan like N'%" + key + "%' UNION SELECT nguyenQuan FROM dbo.tblCV where nguyenQuan like N'%an%' Union select nguyenQuan from (select xa.tenXa +', '+ (select huyen.tenHuyen from dbo.tblDMQuanHuyen as huyen where huyen.maHuyenpr = xa.maHuyenpr_sd) +', '+ (select tinh.tenTinh from dbo.tblDMTinh as tinh where tinh.maTinhpr = xa.maTinhpr_sd) as 'nguyenQuan' from dbo.tblDMXa as xa) as xaHuyenTinh where xaHuyenTinh.nguyenQuan like N'%"+key+ "%') as xaHuyenTinh WHERE nguyenQuan like N'%" + key + "%'";
                DataTable dt = sqlFun.GetData(strSQL);
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string temp = dt.Rows[i][0].ToString();
                            temp = temp.Replace("Xã", "").Trim();
                            temp = temp.Replace("Huyện", "").Trim();
                            temp = temp.Replace("Quận", "Q.").Trim();
                            temp = temp.Replace("Thị trấn", "TT.").Trim();
                            temp = temp.Replace("Phường", "P.").Trim();
                            temp = temp.Replace("Tỉnh", "").Trim();
                            temp = temp.Replace("Thị xã", "TX.").Trim();
                            temp = temp.Replace("Thành phố", "TP.").Trim();
                            dt.Rows[i].SetField(0, temp);
                        }
                        return JsonConvert.SerializeObject(dt, Formatting.Indented);
                    }
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(dt, Formatting.Indented);
                }
                return JsonConvert.SerializeObject(dt, Formatting.Indented);
            }
            catch
            {
                return null;
            }
        }
        
        [WebMethod()]
        public static string LayTuDienNoiCap(string key)
        {
            try
            {
                SqlFunction sqlFun = new SqlFunction();
                string strSQL = @"
                                SELECT DISTINCT TOP 10 tenTinh FROM dbo.tblDMTinh where tenTinh like N'%"+key+"%';";
                DataTable dt = sqlFun.GetData(strSQL);
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string temp = dt.Rows[i][0].ToString();
                            //temp = temp.Replace("Quận", "Q.").Trim();
                            //temp = temp.Replace("Tỉnh", "").Trim();
                            temp = temp.Replace("Thành phố", "TP.").Trim();
                            if (!temp.Contains("TP."))
                                temp = "Tỉnh "+temp;
                            dt.Rows[i].SetField(0, temp);
                        }
                        return JsonConvert.SerializeObject(dt, Formatting.Indented);
                    }
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(dt, Formatting.Indented);
                }
                return JsonConvert.SerializeObject(dt, Formatting.Indented); ;
            }
            catch
            {
                return null;
            }
        }

        [WebMethod()]
        public static string LayDanToc(string key)
        {
            try
            {
                SqlFunction sqlFun = new SqlFunction();
                string strSQL = @"
                                SELECT DISTINCT TOP 10 danToc FROM dbo.tblCV where danToc like N'%" + key + "%';";
                DataTable dt = sqlFun.GetData(strSQL);
                return JsonConvert.SerializeObject(dt, Formatting.Indented); ;
            }
            catch
            {
                return null;
            }
        }

        [WebMethod()]
        public static string LayTonGiao(string key)
        {
            try
            {
                SqlFunction sqlFun = new SqlFunction();
                string strSQL = @"
                                SELECT DISTINCT TOP 10 tonGiao FROM dbo.tblCV where tonGiao like N'%" + key + "%';";
                DataTable dt = sqlFun.GetData(strSQL);
                return JsonConvert.SerializeObject(dt, Formatting.Indented); ;
            }
            catch
            {
                return null;
            }
        }

        [WebMethod()]
        public static string LayTenChuHo(string key)
        {
            try
            {
                SqlFunction sqlFun = new SqlFunction();
                string strSQL = @"SELECT DISTINCT TOP 10 tenChuHoSHK FROM dbo.tblCV where tenChuHoSHK like N'%" + key + "%';";
                DataTable dt = sqlFun.GetData(strSQL);
                return JsonConvert.SerializeObject(dt, Formatting.Indented); ;
            }
            catch
            {
                return null;
            }
        }

        [WebMethod()]
        public static string LayQuanHeChuHo(string key)
        {
            try
            {
                SqlFunction sqlFun = new SqlFunction();
                string strSQL = @"SELECT DISTINCT TOP 10 quanHeChuHoSHK FROM dbo.tblCV where quanHeChuHoSHK like N'%" + key + "%';";
                DataTable dt = sqlFun.GetData(strSQL);
                return JsonConvert.SerializeObject(dt, Formatting.Indented); ;
            }
            catch
            {
                return null;
            }
        }

        [WebMethod()]
        public static string LayThuongTruSHK(string key)
        {
            try
            {
                SqlFunction sqlFun = new SqlFunction();
                string strSQL = @"SELECT DISTINCT TOP 10 diaChiThuongTruSHK FROM dbo.tblCV where diaChiThuongTruSHK like N'%" + key + "%'";
                DataTable dt = sqlFun.GetData(strSQL);
                return JsonConvert.SerializeObject(dt, Formatting.Indented); ;
            }
            catch
            {
                return null;
            }
        }

        [WebMethod()]
        public static string LayTheoHoTen(string key)
        {
            try
            {
                SqlFunction sqlFun = new SqlFunction();
                string strSQL = @"
                                SELECT DISTINCT TOP 10 sttCV, hoTen FROM dbo.tblCV where hoTen like N'%" + key + "%'";
                DataTable dt = sqlFun.GetData(strSQL);
                return JsonConvert.SerializeObject(dt, Formatting.Indented); ;
            }
            catch
            {
                return null;
            }
        }

        [WebMethod()]
        public static string XuaExcelCV(string tuNgay, string denNgay, string loai)
        {
            try
            {
                tuNgay = _mChuyenChuoiSangNgay(tuNgay);
                denNgay = _mChuyenChuoiSangNgay(denNgay);
            }
            catch (Exception)
            {

            }
            string fileName = "CV_" + (DateTime.Now.ToString("ddMMyyyyHHmmss")) + ".xlsx";
            string fileMau = HttpContext.Current.Server.MapPath("~/BaoCao/CVMau.xlsx"); // file excel mẫu
            string fileKQ = HttpContext.Current.Server.MapPath("~/Excel/" + fileName); // file khi xuất
            string url = HttpContext.Current.Server.MapPath("~/Excel/");
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

            File.Copy(fileMau, fileKQ, true);
            var wb = new XLWorkbook(fileKQ);
            var ws = wb.Worksheet(1);
            try
            {
                SqlFunction sqlFun = new SqlFunction();

                string Query = null;
                
                if (loai=="2")
                {
                    Query = @"select * from tblCV cv";
                }
                if (loai=="1")
                {
                   Query= @"select * from tblCV cv where cv.ngayNhapLieu >= '" + tuNgay + "' and cv.ngayNhapLieu <= '" + denNgay + "'";
                }
                DataTable tab = new DataTable();
                tab = sqlFun.GetData(Query);
                int dongXuat = 2;
                int stt = 1;
                
                foreach (DataRow dr in tab.Rows)
                {
                    ws.Cell("A" + dongXuat).Value = stt.ToString();
                    ws.Cell("B" + dongXuat).Value = "";
                    ws.Cell("C" + dongXuat).Value = ""+dr["hoTen"].ToString();
                    ws.Cell("D" + dongXuat).Value = "'"+dr["maCC"].ToString();  
                    ws.Cell("E" + dongXuat).Value = ""+dr["toLam"].ToString(); 
                    ws.Cell("F" + dongXuat).Value = "'"+dr["ngayVaoLam"].ToString();
                    ws.Cell("G" + dongXuat).Value = dr["thamNien"].ToString();
                    
                    ws.Cell("H" + dongXuat).Value = "'"+dr["soDienThoai"].ToString();
                    ws.Cell("I" + dongXuat).Value = dr["nguoiGioiThieu"].ToString();
                    ws.Cell("J" + dongXuat).Value = "";
                    ws.Cell("K" + dongXuat).Value = "'"+dr["ngaySinh"].ToString();
                    ws.Cell("L" + dongXuat).Value = dr["gioiTinh"].ToString();
                    ws.Cell("M" + dongXuat).Value = "'"+dr["soCMND"].ToString();
                    ws.Cell("N" + dongXuat).Value = "'"+dr["ngayCap"].ToString();
                    ws.Cell("O" + dongXuat).Value = "'"+dr["noiCap"].ToString();
                    ws.Cell("P" + dongXuat).Value = dr["nguyenQuan"].ToString();
                    ws.Cell("Q" + dongXuat).Value = dr["danToc"].ToString();
                    ws.Cell("R" + dongXuat).Value = dr["tonGiao"].ToString();
                    ws.Cell("S" + dongXuat).Value = dr["chucDanh"].ToString();
                    ws.Cell("T" + dongXuat).Value = "'"+dr["trinhDoVanHoa"].ToString();
                    ws.Cell("U" + dongXuat).Value = dr["trinhTrangGiaDinh"].ToString();
                    ws.Cell("V" + dongXuat).Value = ""+dr["diaChiThuongTruSHK"].ToString();
                    ws.Cell("W" + dongXuat).Value = dr["diaChiThuongTruCMND"].ToString();
                    ws.Cell("X" + dongXuat).Value = dr["soSHK"].ToString();
                    ws.Cell("Y" + dongXuat).Value = dr["tenChuHoSHK"].ToString();
                    ws.Cell("Z" + dongXuat).Value = dr["quanHeChuHoSHK"].ToString();
                    ws.Cell("AA" + dongXuat).Value = dr["ghiChu"].ToString();
                    ws.Cell("AB" + dongXuat).Value = dr["phanXuong"].ToString();
                    // canh giữa
                    ws.Range("D" + dongXuat).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Range("L" + dongXuat).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Range("Q" + dongXuat).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Range("R" + dongXuat).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Range("S" + dongXuat).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    // canh phải
                    ws.Range("F" + dongXuat).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    ws.Range("H" + dongXuat).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    ws.Range("K" + dongXuat).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    ws.Range("M" + dongXuat).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    ws.Range("N" + dongXuat).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    ws.Range("T" + dongXuat).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    ws.Range("X" + dongXuat).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    // CANH TRÁI
                    ws.Range("AA" + dongXuat).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    ++dongXuat;
                    ++stt;
                }
            }
            catch(Exception ex)
            {

            }
            wb.Save();
            return "/Excel/" + fileName; // XUẤT ra đường dẫn file
        }
        public static string _mChuyenChuoiSangNgay(string ddMMyyyy)
        {
            return ddMMyyyy.Substring(3, 2) + "/" + ddMMyyyy.Substring(0, 2) + "/" + ddMMyyyy.Substring(6, 4);
        }
    }
}
