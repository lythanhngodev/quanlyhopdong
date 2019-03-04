<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="quanlyhopdong.aspx.cs" Inherits="quanlyhopdong.quanly.quanlyhopdong" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<asp:Content ID="contentHead1" ContentPlaceHolderID="head" runat="server" >
    <title>Quản lý hợp đồng</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .colXoa {
            padding: 4px 2px 1px 2px !important;
            text-align: left !important;
            border-left: 0px !important;
        }

        .btn-group-xs > .btn, .btn-xs {
            line-height: 15px !important;
            padding-left: 6px !important;
            padding-right: 6px !important;
            border: 1px;
        }

        .colSua {
            padding: 4px 2px 1px 2px !important;
            border-right: 0px !important;
            text-align: right !important;
        }

        th, td {
            vertical-align: middle !important;
        }

        .dataTables_length select {
            display: contents;
        }

        @media (max-width: 991px) {
            .modal-dialog {
                width: 100%;
            }

            .page-content {
                padding-top: 49px !important;
            }
        }

        @media (max-width: 639px) {
            .dataTables_wrapper .col-xs-6 {
                width: 100% !important;
                text-align: center !important;
            }

            .dataTables_filter, .dataTables_paginate {
                text-align: center !important;
                display: block;
            }
        }

        @media (min-width: 992px) {
            .modal-dialog {
                width: 40%;
            }
        }
        .toolbar{
            float: right;
        }
        .customInput{
            width: 200px !important;
            float: left;
        }
        #ContentPlaceHolder1_AsyncFileUpload2_ctl02
        {
            display: none !important;
        }
    </style>
    <section class="content container-fluid">
        <div class="breadcrumbs breadcrumbs-fixed" id="breadcrumbs">
            <h4 style="margin:0">QUẢN LÝ HỢP ĐỒNG</h4>
            <hr style="margin: 10px 0;" />
            <div class="toolbar" id="divToolbarThemMoi" style="display: block;">
                <button id="btnThemExcel" class="btn btn-primary" type="button">
                    <i class="fa fa-table"></i>
                    <span class="hidden-320">Nhập Excel</span>
                </button>
                <button id="btnChonTuCV" class="btn btn-primary" type="button">
                    <i class="fa fa-plus"></i>
                    <span class="hidden-320">Nhập từ CV</span>
                </button>
                <button id="btnThem" class="btn btn-primary" type="button">
                    <i class="fa fa-plus"></i>
                    <span class="hidden-320">Thêm mới</span>
                </button>
            </div>
            <div class="toolbar" id="divToolLuuQuyLai" style="display: none;">
                <button id="btnThemHoanTat" class="btn btn-primary" type="button" onclick="btnLuuDuLieu(this,'1')">
                    <i class="ace-icon fa fa-floppy-o bigger-110"></i>
                    <span class="hidden-320">Lưu và đóng</span>
                </button>
                <button id="btnLuuVaThem" class="btn btn-success" type="button" onclick="btnLuuDuLieu(this,'2')">
                    <i class="fa fa-check"></i>
                    <span class="hidden-320">Lưu và thêm mới</span>
                </button>
                <button id="btnQuayLai" class="btn btn-danger" type="button">
                    <i class="fa fa-arrow-left"></i>
                    <span class="hidden-320">Quay lại</span>
                </button>
            </div>
        </div>
    </section>
    <section class="content" style="padding:0">
        <div class="col-xs-12" id="divload" style="display:none;">
            <div id="div1">
                <div class="col-xs-12">
                    <div class="row" style="padding-bottom: 2px; padding-top: 5px">
                        <div class="col-md-8" style="padding: 0;">
                            <select class="form-control customInput" id="selNamThang" style="margin-left: 6px;margin-right: 10px;">
                                <option value="">--Chọn thời gian--</option>
                            </select>
                            <button class="btn btn-warning btn-sm" onclick="XuatWordHD();return false;" ><i class="fa fa-file-word-o" aria-hidden="true" ></i>&ensp;Xuất Word</button>
                            <button class="btn btn-warning btn-sm" onclick="XuatExcelHD();return false;" ><i class="fa fa-file-excel-o" aria-hidden="true" ></i>&ensp;Xuất Excel</button>
                        </div>
                        <div class="col-md-4" style="float: right;padding: 0;">
                            <input  style="width: 200px; float: right;" type="text" placeholder="Nội dung tìm kiếm..." onkeyup="searchValue(Grid1,0,this.value)" class="form-control searchCss">
                        </div>
                    </div>
                </div>
                <div>
                    <cc1:Grid ID="Grid1" runat="server" FolderStyle="~/App_Themes/Styles/style_7" AllowPaging="true" FilterType="ProgrammaticOnly"
                        PageSizeOptions="15,50,150,200,500,1000,-1" PageSize="-1" AutoGenerateColumns="false" AllowFiltering="true"
                        EnableRecordHover="false" AllowGrouping="false" OnRebind="Grid1_Rebind" Width="100%" Height="400" AllowColumnResizing="true"
                        AllowAddingRecords="false" AllowMultiRecordSelection="true">
                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="450" NumberOfFixedColumns="1" />
                        <PagingSettings Position="Bottom" />
                        <FilteringSettings MatchingType="AnyFilter" FilterPosition="Top" FilterLinksPosition="Bottom" />
                        <Columns>
                            <cc1:Column HeaderText="Thao tác" AllowSorting="false" Align="Center" Width="70px" DataField="thaoTac" ParseHTML="true">
                            </cc1:Column>
                            <cc1:Column DataField="soHopDong" HeaderText="Số HĐ" Width="100" Visible="True" runat="server" Wrap="true" Align="Center">
                            </cc1:Column>
                            <cc1:Column DataField="hoTen" HeaderText="Họ Tên" Width="200" Visible="True" runat="server" Wrap="true">
                            </cc1:Column>
                            <cc1:Column DataField="ngayVaoLam" HeaderText="Ngày vào làm" Width="100" DataFormatString="{0:dd/MM/yyyy}" Align="Right">
                            </cc1:Column>
                            <cc1:Column DataField="ngayKy" HeaderText="Ngày ký HĐ" Width="100" DataFormatString="{0:dd/MM/yyyy}" Align="Right">
                            </cc1:Column>
                            <cc1:Column DataField="ngayKetThucHD" HeaderText="Ngày KT HĐ" Width="100" DataFormatString="{0:dd/MM/yyyy}" Align="Right">
                            </cc1:Column>
                            <cc1:Column DataField="luongCoBan" HeaderText="Lương CB" Width="100" DataFormatString="{0:#,# VNĐ}" Align="Right">
                            </cc1:Column>
                            <cc1:Column DataField="ngaySinh" HeaderText="Ngày sinh" Width="100" Align="Right" Visible="True" runat="server" Wrap="true">
                            </cc1:Column>
                            <cc1:Column DataField="gioiTinh" HeaderText="Giới tính" Width="100" Align="Center" Visible="True" runat="server" Wrap="true">
                            </cc1:Column>
                            <cc1:Column DataField="soCMND" HeaderText="CMND" Width="100" Visible="True" runat="server" Wrap="true" Align="Right">
                            </cc1:Column>
                            <cc1:Column DataField="ngayCap" HeaderText="Ngày cấp" Width="100" Visible="True" Align="Right" runat="server" Wrap="true">
                            </cc1:Column>
                            <cc1:Column DataField="noiCap" HeaderText="Nơi cấp" Width="120" Visible="True" Align="Center" runat="server" Wrap="true">
                            </cc1:Column>
                            <cc1:Column DataField="nguyenQuan" HeaderText="Nguyên quán" Width="300" Visible="True" runat="server" Wrap="true">
                            </cc1:Column>
                            <cc1:Column DataField="danToc" HeaderText="Dân tộc" Width="100" Visible="True" runat="server" Wrap="true" Align="Center">
                            </cc1:Column>
                            <cc1:Column DataField="tonGiao" HeaderText="Tôn giáo" Width="110" Visible="True" Align="Center" runat="server" Wrap="true">
                            </cc1:Column>
                            <cc1:Column DataField="chucDanh" HeaderText="Chức danh" Width="120" Visible="True" Align="Center" runat="server" Wrap="true">
                            </cc1:Column>
                            <cc1:Column DataField="trinhDoVanHoa" HeaderText="Trình độ VH" Width="100" Visible="True" runat="server" Wrap="true" Align="Right">
                            </cc1:Column>
                        </Columns>
                        <LocalizationSettings CancelAllLink="Hủy tất cả" AddLink="Thêm mới" CancelLink="Hủy"
                            DeleteLink="Xóa" EditLink="Sửa" Filter_ApplyLink="Tìm kiếm" Filter_HideLink="Đóng tìm kiếm"
                            Filter_RemoveLink="Xóa tìm kiếm" Filter_ShowLink="Mở tìm kiếm" FilterCriteria_NoFilter="Không tìm kiếm"
                            FilterCriteria_Contains="Chứa" FilterCriteria_DoesNotContain="Không chứa" FilterCriteria_StartsWith="Bắt đầu với"
                            FilterCriteria_EndsWith="Kết thúc với" FilterCriteria_EqualTo="Bằng" FilterCriteria_NotEqualTo="Không bằng"
                            FilterCriteria_SmallerThan="Nhỏ hơn" FilterCriteria_GreaterThan="Lớn hơn" FilterCriteria_SmallerThanOrEqualTo="Nhỏ hơn hoặc bằng"
                            FilterCriteria_GreaterThanOrEqualTo="Lớn hơn hoặc bằng" FilterCriteria_IsNull="Rỗng"
                            FilterCriteria_IsNotNull="Không rỗng" FilterCriteria_IsEmpty="Trống" FilterCriteria_IsNotEmpty="Không trống"
                            Paging_OfText="của" Grouping_GroupingAreaText="Kéo tiêu đề cột vào đây để loại theo cột đó"
                            JSWarning="Có một lỗi khởi tạo lưới với ID '[GRID_ID]'. \ N \ n [Chú ý] \ n \ nHãy liên hệ bộ phận bảo trì của Nhất Tâm Soft để được giúp đỡ."
                            LoadingText="Đang tải...." MaxLengthValidationError="Giá trị mà bạn đã nhập vào trong cột XXXXX vượt quá số lượng tối đa ký tự YYYYY cho phép cột này."
                            ModifyLink="Chỉnh sửa" NoRecordsText="Không có dữ liệu" Paging_ManualPagingLink="Trang kế »"
                            Paging_PageSizeText="Số dòng 1 trang:" Paging_PagesText="Trang:" Paging_RecordsText="Dòng:"
                            ResizingTooltipWidth="Rộng:" SaveAllLink="Lưu tất cả" SaveLink="Lưu" TypeValidationError="Giá trị mà bạn đã nhập vào trong cột XXXXX là không đúng."
                            UndeleteLink="Không xóa" UpdateLink="Lưu" />
                    </cc1:Grid>
                </div>
            </div>
            <div id="div2" style="display:none;">
                <div class="col-xs-12">
                    <div class="row">
                        <div class="col-md-12">
                            <h4 class="header smaller lighter blue" id="TieuDeDiv2" style="margin-top: 0;">
                                Thêm mới hợp đồng
                            </h4>
                        </div>
                    </div>

                    <fieldset style="border: 1px solid #DBDBE1; margin: 0;border-radius: 20px;">
                        <%--<legend><b>Thông tin CV</b></legend>--%>
                        <br />
                        <div class="row" style="padding-bottom: 5px">
                            <div class="col-md-4">
                                <div class="col-md-4">
                                    <label>Số hợp đồng </label>
                                </div>
                                <div class="col-md-8" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtSoHD" placeholder="Nhập số hợp đồng">
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px">
                            <div class="col-md-4">
                                <div class="col-md-4">
                                    <label>Họ và tên </label>
                                </div>
                                <div class="col-md-8" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtHoTen" placeholder="Nhập họ và tên">
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-4">
                                    <label>Tổ</label>
                                </div>
                                <div class="col-md-8" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtTo" placeholder="Nhập tổ">
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-4">
                                    <label>Ngày vào làm</label>
                                </div>
                                <div class="col-md-8" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtNgayVaoLam" placeholder="dd/MM/yyyy">
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px">
                            <div class="col-md-4">
                                <div class="col-md-4">
                                    <label>Ngày ký HĐ</label>
                                </div>
                                <div class="col-md-8" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtNgayKyHD" placeholder="dd/MM/yyyy">
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-4">
                                    <label>Ngày KT HĐ</label>
                                </div>
                                <div class="col-md-8" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtNgayKetThucHD" placeholder="dd/MM/yyyy">
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-4">
                                    <label>Lương cơ bản</label>
                                </div>
                                <div class="col-md-8" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtLuongCoBan" placeholder="Nhập lương cơ bản">
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px">
                            <div class="col-md-4">
                                <div class="col-md-4">
                                    <label>Số BHXH</label>
                                </div>
                                <div class="col-md-8" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtSoBHXH" placeholder="Nhập số sổ BHXH">
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-4">
                                    <label>Ngày sinh</label>
                                </div>
                                <div class="col-md-8" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtNgaySinh" placeholder="dd/MM/yyyy">
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-4">
                                    <label>Giới tính</label>
                                </div>
                                <div class="col-md-8" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtGioiTinh" placeholder="Nhập giới tính">
                                </div>
                            </div>
                        </div>

                        <div class="row" style="padding-bottom: 5px">
                            <div class="col-md-4">
                                <div class="col-md-4">
                                    <label>Số CMND</label>
                                </div>
                                <div class="col-md-8" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtCMND" placeholder="Nhập CMND">
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-4">
                                    <label>Ngày cấp</label>
                                </div>
                                <div class="col-md-8" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtNgayCap" placeholder="dd/MM/yyyy">
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-4">
                                    <label>Nơi cấp</label>
                                </div>
                                <div class="col-md-8" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtNoiCap" placeholder="Nhập nơi cấp">
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px">
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label>Nguyên quán</label>
                                </div>
                                <div class="col-md-9" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtQueQuan" placeholder="Nhập nguyên quán trong CMND">
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px">
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label>Dân tộc</label>
                                </div>
                                <div class="col-md-9" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtDanToc" placeholder="Nhập dân tộc">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label>Tôn giáo</label>
                                </div>
                                <div class="col-md-9" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtTonGiao" placeholder="Nhập tôn giáo">
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px">
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label>Chức danh</label>
                                </div>
                                <div class="col-md-9" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtChucDanh" placeholder="Nhập chức danh">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label>Trình độ</label>
                                </div>
                                <div class="col-md-9" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtTrinhDoVanHoa" placeholder="Nhập trình độ văn hóa">
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px">
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label>Tình trạng GĐ</label>
                                </div>
                                <div class="col-md-9" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtTinhTrangGiaDinh" placeholder="Nhập tình trạng gia đình">
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px">
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label>ĐC.TT (SHK)</label>
                                </div>
                                <div class="col-md-9" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtDiaChiThuongTru" placeholder="Nhập địa chỉ thường trú SHK">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label>ĐC (CMND)</label>
                                </div>
                                <div class="col-md-9" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtDiaChiThuongTruCMND" placeholder="Nhập địa chỉ thường trú CMND">
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px">
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label>Số hộ khẩu</label>
                                </div>
                                <div class="col-md-9" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtSoHoKhau" placeholder="Nhập số hộ khẩu">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label>Tên CH</label>
                                </div>
                                <div class="col-md-9" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtTenChuHo" placeholder="Nhập tên chủ hộ">
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px">
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label>Quan hệ CH</label>
                                </div>
                                <div class="col-md-9" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtQuanHeChuHo" placeholder="Nhập quan hệ chủ hộ">
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px">
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label>Ghi chú</label>
                                </div>
                                <div class="col-md-9" style="padding-left: 6px;">
                                    <input class="form-control input-sm" type="text" id="txtGhiChu" placeholder="Nhập ghi chú">
                                </div>
                            </div>
                        </div>
                        <input type="hidden" id="txtHdMaHD" />
                        <br />
                    </fieldse>
                </div>
            </div>
        </div>

        <div id="mdNhapExcel" class="modal fade" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" style="width: 90% !important;">
                <div class="modal-content">
                    <div class="modal-header no-padding">
                        <div class="table-header" style="font-size: 20px; padding: 10px;">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <span class="white">&times;</span>
                            </button>
                            <span>Nhập hợp đồng từ excel</span>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row" style="padding-bottom: 2px">
                            <div class="col-md-2">
                                <label>Tập tin đính kèm </label>
                            </div>
                            <div class="col-md-8">
                                <input type="file" class="form-control input-sm" id="fileExcel" />
                            </div>
                            <div class="col-md-2">
                                <button id="btnNhapExcelTap" class="btn btn-sm btn-primary">Xem trước</button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <cc1:Grid ID="Grid2" runat="server" FolderStyle="~/App_Themes/Styles/style_7" AllowPaging="true" FilterType="ProgrammaticOnly"
                                    PageSizeOptions="15,50,150,200,500,1000,-1" PageSize="-1" AutoGenerateColumns="false" AllowFiltering="true"
                                    EnableRecordHover="false" AllowGrouping="false" OnRebind="Grid2_Rebind" Width="100%" Height="400" AllowColumnResizing="true"
                                    AllowAddingRecords="false" AllowMultiRecordSelection="true">
                                    <ScrollingSettings ScrollWidth="100%" ScrollHeight="450" NumberOfFixedColumns="1" />
                                    <PagingSettings Position="Bottom" />
                                    <FilteringSettings MatchingType="AnyFilter" FilterPosition="Top" FilterLinksPosition="Bottom" />
                                    <Columns>
                                        <cc1:Column DataField="hoTen" HeaderText="Họ Tên" Width="200" Visible="True" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="ngayVaoLam" HeaderText="Ngày vào làm" Width="100" DataFormatString="{0:dd/MM/yyyy}" Align="Right">
                                        </cc1:Column>
                                        <cc1:Column DataField="ngayKy" HeaderText="Ngày ký HĐ" Width="100" DataFormatString="{0:dd/MM/yyyy}" Align="Right">
                                        </cc1:Column>
                                        <cc1:Column DataField="ngayKetThucHD" HeaderText="Ngày KT HĐ" Width="100" DataFormatString="{0:dd/MM/yyyy}" Align="Right">
                                        </cc1:Column>
                                        <cc1:Column DataField="ngaySinh" HeaderText="Ngày sinh" Width="100" Align="Right" Visible="True" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="gioiTinh" HeaderText="Giới tính" Width="100" Align="Center" Visible="True" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="soCMND" HeaderText="CMND" Width="100" Visible="True" runat="server" Wrap="true" Align="Right">
                                        </cc1:Column>
                                        <cc1:Column DataField="ngayCap" HeaderText="Ngày cấp" Width="100" Visible="True" Align="Right" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="noiCap" HeaderText="Nơi cấp" Width="120" Visible="True" Align="Center" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="nguyenQuan" HeaderText="Nguyên quán" Width="300" Visible="True" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="danToc" HeaderText="Dân tộc" Width="100" Visible="True" runat="server" Wrap="true" Align="Center">
                                        </cc1:Column>
                                        <cc1:Column DataField="tonGiao" HeaderText="Tôn giáo" Width="110" Visible="True" Align="Center" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="chucDanh" HeaderText="Chức danh" Width="120" Visible="True" Align="Center" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="trinhDoVanHoa" HeaderText="Trình độ VH" Width="100" Visible="True" runat="server" Wrap="true" Align="Right">
                                        </cc1:Column>
                                    </Columns>
                                    <LocalizationSettings CancelAllLink="Hủy tất cả" AddLink="Thêm mới" CancelLink="Hủy"
                                        DeleteLink="Xóa" EditLink="Sửa" Filter_ApplyLink="Tìm kiếm" Filter_HideLink="Đóng tìm kiếm"
                                        Filter_RemoveLink="Xóa tìm kiếm" Filter_ShowLink="Mở tìm kiếm" FilterCriteria_NoFilter="Không tìm kiếm"
                                        FilterCriteria_Contains="Chứa" FilterCriteria_DoesNotContain="Không chứa" FilterCriteria_StartsWith="Bắt đầu với"
                                        FilterCriteria_EndsWith="Kết thúc với" FilterCriteria_EqualTo="Bằng" FilterCriteria_NotEqualTo="Không bằng"
                                        FilterCriteria_SmallerThan="Nhỏ hơn" FilterCriteria_GreaterThan="Lớn hơn" FilterCriteria_SmallerThanOrEqualTo="Nhỏ hơn hoặc bằng"
                                        FilterCriteria_GreaterThanOrEqualTo="Lớn hơn hoặc bằng" FilterCriteria_IsNull="Rỗng"
                                        FilterCriteria_IsNotNull="Không rỗng" FilterCriteria_IsEmpty="Trống" FilterCriteria_IsNotEmpty="Không trống"
                                        Paging_OfText="của" Grouping_GroupingAreaText="Kéo tiêu đề cột vào đây để loại theo cột đó"
                                        JSWarning="Có một lỗi khởi tạo lưới với ID '[GRID_ID]'. \ N \ n [Chú ý] \ n \ nHãy liên hệ bộ phận bảo trì của Nhất Tâm Soft để được giúp đỡ."
                                        LoadingText="Đang tải...." MaxLengthValidationError="Giá trị mà bạn đã nhập vào trong cột XXXXX vượt quá số lượng tối đa ký tự YYYYY cho phép cột này."
                                        ModifyLink="Chỉnh sửa" NoRecordsText="Không có dữ liệu" Paging_ManualPagingLink="Trang kế »"
                                        Paging_PageSizeText="Số dòng 1 trang:" Paging_PagesText="Trang:" Paging_RecordsText="Dòng:"
                                        ResizingTooltipWidth="Rộng:" SaveAllLink="Lưu tất cả" SaveLink="Lưu" TypeValidationError="Giá trị mà bạn đã nhập vào trong cột XXXXX là không đúng."
                                        UndeleteLink="Không xóa" UpdateLink="Lưu" />
                                </cc1:Grid>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="btnTienHanhNhap" class="btn btn-success" type="button">
                            <i class="fa fa-check"></i>
                            <span class="hidden-320">Lưu và đóng</span>
                        </button>
                        <a href="#" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-close"></i>&nbsp;Đóng</a>

                    </div>
                </div>
            </div>
        </div>

        <div id="mdNhapTuCV" class="modal fade" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" style="width: 90% !important;">
                <div class="modal-content">
                    <div class="modal-header no-padding">
                        <div class="table-header" style="font-size: 20px; padding: 10px;">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <span class="white">&times;</span>
                            </button>
                            <span>Nhập hợp đồng từ CV</span>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row" style="padding-bottom: 2px">
                            <div class="col-md-2">
                                <button id="btnNhapTuCV" class="btn btn-sm btn-success">Nhập CV đã chọn</button>
                                <button id="btnDongNhapTuCV" data-dismiss="modal" class="btn btn-sm btn-danger">Đóng</button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <cc1:Grid ID="Grid3" runat="server" FolderStyle="~/App_Themes/Styles/style_7" AllowPaging="true" FilterType="ProgrammaticOnly"
                                    PageSizeOptions="15,50,150,200,500,1000,-1" PageSize="-1" AutoGenerateColumns="false" AllowFiltering="true"
                                    EnableRecordHover="false" AllowGrouping="false" OnRebind="Grid3_Rebind" Width="100%" Height="400" AllowColumnResizing="true"
                                    AllowAddingRecords="false" AllowMultiRecordSelection="true">
                                    <ScrollingSettings ScrollWidth="100%" ScrollHeight="450" NumberOfFixedColumns="1" />
                                    <PagingSettings Position="Bottom" />
                                    <FilteringSettings MatchingType="AnyFilter" FilterPosition="Top" FilterLinksPosition="Bottom" />
                                    <Columns>
                                        <cc1:CheckBoxSelectColumn Width="40" ShowHeaderCheckBox="true" Align="center" HeaderAlign="center">
                                        </cc1:CheckBoxSelectColumn>
                                        <cc1:Column DataField="sttCV" HeaderText="MCC" Width="90" Visible="false">
                                        </cc1:Column>
                                        <cc1:Column DataField="maCC" HeaderText="MCC" Width="90">
                                        </cc1:Column>
                                        <cc1:Column DataField="hoTen" HeaderText="Họ Tên" Width="150" Visible="True" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="ngayVaoLam" HeaderText="Ngày vào làm" Width="100" DataFormatString="{0:dd/MM/yyyy}" Align="Right">
                                        </cc1:Column>
                                        <cc1:Column DataField="soDienThoai" HeaderText="Số điện thoại" Width="100" Visible="True" runat="server" Wrap="true" Align="Right">
                                        </cc1:Column>
                                        <cc1:Column DataField="nguoiGioiThieu" HeaderText="Người giới thiệu" Width="200">
                                        </cc1:Column>
                                        <cc1:Column DataField="ngaySinh" HeaderText="Ngày sinh" Width="100" Align="Right" Visible="True" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="gioiTinh" HeaderText="Giới tính" Width="100" Align="Center" Visible="True" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="soCMND" HeaderText="CMND" Width="100" Visible="True" runat="server" Wrap="true" Align="Right">
                                        </cc1:Column>
                                        <cc1:Column DataField="ngayCap" HeaderText="Ngày cấp" Width="100" Visible="True" Align="Right" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="noiCap" HeaderText="Nơi cấp" Width="120" Visible="True" Align="Center" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="nguyenQuan" HeaderText="Nguyên quán" Width="300" Visible="True" runat="server" Wrap="true" Align="Right">
                                        </cc1:Column>
                                        <cc1:Column DataField="danToc" HeaderText="Dân tộc" Width="100" Visible="True" runat="server" Wrap="true" Align="Center">
                                        </cc1:Column>
                                        <cc1:Column DataField="tonGiao" HeaderText="Tôn giáo" Width="110" Visible="True" Align="Center" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="chucDanh" HeaderText="Chức danh" Width="120" Visible="True" Align="Center" runat="server" Wrap="true">
                                        </cc1:Column>
                                        <cc1:Column DataField="trinhDoVanHoa" HeaderText="Trình độ VH" Width="100" Visible="True" runat="server" Wrap="true" Align="Right">
                                        </cc1:Column>
                                    </Columns>
                                    <LocalizationSettings CancelAllLink="Hủy tất cả" AddLink="Thêm mới" CancelLink="Hủy"
                                        DeleteLink="Xóa" EditLink="Sửa" Filter_ApplyLink="Tìm kiếm" Filter_HideLink="Đóng tìm kiếm"
                                        Filter_RemoveLink="Xóa tìm kiếm" Filter_ShowLink="Mở tìm kiếm" FilterCriteria_NoFilter="Không tìm kiếm"
                                        FilterCriteria_Contains="Chứa" FilterCriteria_DoesNotContain="Không chứa" FilterCriteria_StartsWith="Bắt đầu với"
                                        FilterCriteria_EndsWith="Kết thúc với" FilterCriteria_EqualTo="Bằng" FilterCriteria_NotEqualTo="Không bằng"
                                        FilterCriteria_SmallerThan="Nhỏ hơn" FilterCriteria_GreaterThan="Lớn hơn" FilterCriteria_SmallerThanOrEqualTo="Nhỏ hơn hoặc bằng"
                                        FilterCriteria_GreaterThanOrEqualTo="Lớn hơn hoặc bằng" FilterCriteria_IsNull="Rỗng"
                                        FilterCriteria_IsNotNull="Không rỗng" FilterCriteria_IsEmpty="Trống" FilterCriteria_IsNotEmpty="Không trống"
                                        Paging_OfText="của" Grouping_GroupingAreaText="Kéo tiêu đề cột vào đây để loại theo cột đó"
                                        JSWarning="Có một lỗi khởi tạo lưới với ID '[GRID_ID]'. \ N \ n [Chú ý] \ n \ nHãy liên hệ bộ phận bảo trì của Nhất Tâm Soft để được giúp đỡ."
                                        LoadingText="Đang tải...." MaxLengthValidationError="Giá trị mà bạn đã nhập vào trong cột XXXXX vượt quá số lượng tối đa ký tự YYYYY cho phép cột này."
                                        ModifyLink="Chỉnh sửa" NoRecordsText="Không có dữ liệu" Paging_ManualPagingLink="Trang kế »"
                                        Paging_PageSizeText="Số dòng 1 trang:" Paging_PagesText="Trang:" Paging_RecordsText="Dòng:"
                                        ResizingTooltipWidth="Rộng:" SaveAllLink="Lưu tất cả" SaveLink="Lưu" TypeValidationError="Giá trị mà bạn đã nhập vào trong cột XXXXX là không đúng."
                                        UndeleteLink="Không xóa" UpdateLink="Lưu" />
                                </cc1:Grid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="mdKetQua" class="modal fade" data-backdrop="static" data-keyboard="false" >
            <div class="modal-dialog" style="width: 400px;">
                <div class="modal-content">
                    <div class="modal-header no-padding">
                        <div class="table-header" style="font-size:20px;padding:10px;">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <span class="white">&times;</span>
                            </button>
                            <span id="tieuDeNoiDung">Thông tin file xuất</span>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row" style="padding-bottom: 2px">
                            <div class="col-md-12">
                                <center>Soạn hợp đồng thành công có thể tải</center>
                                <center><a href="" id="linkWord" class="btn btn-success btn-sm"><i class="fa fa-file-excel-o" aria-hidden="true"></i>&ensp;Tại đây</a></center>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a href="#" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-close"></i>&nbsp;Đóng</a>
                    </div>
                </div>
            </div>
        </div>

    </section>
    <script src="../lte/bower_components/jquery/dist/jquery.min.js"></script>
    <script src="../lab/js/jquery-ui.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    
    <script src="../Scripts/quanly/quanlyhopdong.js"></script>
</asp:Content>
