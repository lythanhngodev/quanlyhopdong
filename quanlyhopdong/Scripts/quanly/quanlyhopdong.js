function setMaxHeightModal() {
    $('.modal-body').css('max-height', $(window).height() - 150);
    $('.modal-body').css('overflow-y', 'auto');
}
// kiểu trả về hợp lệ: string, boolean, json
// getAjax("string","../../View/DanhMuc/dmtesst.aspx/ThemDuLieu", { 'string, boolean',_arrT: mangA, donVi: donVi });
function getAjax(kieuTraVe, duongDanAjax, duLieuGui) {
    var result = null;
    $.ajax({
        type: "POST",
        url: duongDanAjax,
        data: JSON.stringify(duLieuGui),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            result = response.d;
        },
        error: function (response) {
            result = null;
            alert('Lỗi kết nối');
        }
    });

    if (result === null) return null;
    // xử lý kiểu trả về
    try {
        switch (kieuTraVe.toLowerCase()) {
            case "string":
                return result.toString();
                break;
            case "boolean":
                try {
                    return JSON.parse((result).toString().toLowerCase());
                } catch (e) {
                    return false;
                }
                break;
            case 'json':
                return JSON.parse(result.toString());
                break;
            default:
                return null;
                break;
        }
    } catch (e) {
        return null;
    }
}
$("#txtQueQuan").autocomplete({
    source: function (request, response) {
        var data = getAjax('json', '../quanly/quanlycv.aspx/LayTuDienNguyenQuan', { key: $('#txtQueQuan').val() });
        response($.map(data, function (item) {
            return {
                label: item.nguyenQuan,
                value: item.nguyenQuan
            }
        }));
    },
    minlength: 2,
    select: function (event, ui) {

    }
});
$("#txtNoiCap").autocomplete({
    source: function (request, response) {
        var data = getAjax('json', '../quanly/quanlycv.aspx/LayTuDienNoiCap', { key: $('#txtNoiCap').val() });
        response($.map(data, function (item) {
            return {
                label: item.tenTinh,
                value: item.tenTinh
            }
        }));
    },
    minlength: 2,
    select: function (event, ui) {

    }
});

$("#txtDiaChiThuongTru").autocomplete({
    source: function (request, response) {
        var data = getAjax('json', '../quanly/quanlycv.aspx/LayThuongTruSHK', { key: $('#txtDiaChiThuongTru').val() });
        response($.map(data, function (item) {
            return {
                label: item.diaChiThuongTruSHK,
                value: item.diaChiThuongTruSHK
            }
        }));
    },
    minlength: 2,
    select: function (event, ui) {

    }
});

$(function () {
    layToanBoDuLieu();
    $('#txtTuNgay,#txtDenNgay').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yyyy'
    });
    $('#meHD').addClass('active');
    laythangnhaphopdong();
});
laythangnhaphopdong = () => {
    $('#selNamThang').find('option').remove();
    var namThangNhapHopDong = getAjax('json', '../quanly/quanlyhopdong.aspx/LayThangNamNhapHopDong', {});
    if (namThangNhapHopDong != null) {
        for (x in namThangNhapHopDong) {
            if (namThangNhapHopDong[x] != undefined) {
                console.log(namThangNhapHopDong[x].ngayNhapLieu.split('.'));
                $('#selNamThang').append($('<option>', {
                    value: namThangNhapHopDong[x].ngayNhapLieu.split('.'),
                    text: 'Tháng ' + namThangNhapHopDong[x].ngayNhapLieu.split('.')[1] + ' năm ' + namThangNhapHopDong[x].ngayNhapLieu.split('.')[0]
                }))
            }
        }
    }
}
var dulieu;
function layToanBoDuLieu() {
    $("#Loadding").show();
    Grid1.refresh();
    Grid2.refresh();
    $('#divload').show();
    $("#Loadding").hide();
}
$('#btnThemExcel').click(function(){
    $('#mdNhapExcel').modal('show');
    setTimeout(function () { Grid2.refresh(); }, 200);
});

$(document).on('click', '#btnNhapExcelTap', () => {
    var file_data = null;
    debugger;
    try {
        file_data = $('#fileExcel').prop('files')[0];
    } catch (e) {
        tbdanger('Vui lòng chọn file Excel');
    }
    if (jQuery.isEmptyObject(file_data)) {
        tbdanger('Vui lòng chọn file Excel')
        return !1;
    }
    var type = file_data.type;
    var match = ["application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"];
    if (type == match[0] || type == match[1]) {
        var form_data = new FormData();
        form_data.append('data', file_data);
        $.ajax({
            type: 'post',
            url: '../quanly/nhapexcelhopdong.ashx',
            data: form_data,
            dataType: 'text',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                Grid2.refresh();
            },
            error: function (error) {
                alert("errror");
            }
        });
    } else {
        tbdanger('Vui lòng chọn file Excel');
    }
    return !1;
});
$(document).on('click', '#btnTienHanhNhap', function () {
    if (getAjax('boolean', '../quanly/quanlyhopdong.aspx/KiemTraNhapExcelHopDong', {})) {
        tbsuccess('Nhập excel thành công');
        $('#mdNhapExcel').modal('hide');
        Grid1.refresh();
    }
    else {
        tbdanger('Có lỗi trong quá trình xử lý dữ liệu, vui lòng thao tác lại');
    }
    return false;
});
$(document).on('click', '#btnChonTuCV', () => {
    $('#mdNhapTuCV').modal('show');
    setTimeout(function () {
        Grid3.refresh();
    }, 200);
    for (var i = 0; i < Grid3.Rows.length; i++) {
        Grid3.deselectRecord(i);
    }
});
$(document).on('click', '#btnNhapTuCV', () => {
    if (jQuery.isEmptyObject(Grid3.SelectedRecords)) {
        tbdanger('Không có CV nào được chọn. Vui lòng chọn CV');
        return !1;
    }
    var mangCacDongChon = Grid3.SelectedRecords;
    var sttCV = [];
    for (var i = 0; i < mangCacDongChon.length; i++) {
        sttCV.push(mangCacDongChon[i].sttCV);
    }
    var data = getAjax('string', '../quanly/quanlyhopdong.aspx/NhapTuCVDaChon', { sttCV: sttCV });
    if (data=="Không có CV nào được chọn") {
        tbdanger("Không có CV nào được chọn. Vui lòng chọn CV");
    } else {
        tbsuccess(data);
        $('#mdNhapTuCV').modal('hide');
    }
    Grid1.refresh();
    laythangnhaphopdong();
    return !1;
});
// thêm mới
$('#btnThem').click(function () {
    $("#TieuDeDiv2").text('Thêm mới hợp đồng');
    $('#div1').css('display', 'none');
    $('#divToolbarThemMoi').css('display', 'none');
    $('#divToolLuuQuyLai').css('display', 'block');
    $('#div2').css('display', 'block');
    TruocKhiThem();
});
// quay lại
$('#btnQuayLai').click(function () {
    var check = !0, check2 = !1;
    $('#div2').find('input').each(function () {
        if ($(this).val() != "" && $(this).val().length > 0) {
            check2 = !0;
        }
    });
    if (check2) {
        var cof = confirm('Hiện có dữ chưa lưu. Bạn có muốn thoát?');
        if (!cof) {
            check = !1;
        }
    }
    if (check) {
        $('#div1').css('display', 'block');
        $('#divToolbarThemMoi').css('display', 'block');
        $('#divToolLuuQuyLai').css('display', 'none');
        $('#div2').css('display', 'none');
        Grid1.refresh();
    }
});
function hienDiv2() {
    $("#TieuDeDiv2").text('Thêm mới CV');
    $('#div1').css('display', 'none');
    $('#divToolbarThemMoi').css('display', 'none');
    $('#divToolLuuQuyLai').css('display', 'block');
    $('#div2').css('display', 'block');
}
function hienDiv1() {
    $('#div1').css('display', 'block');
    $('#divToolbarThemMoi').css('display', 'block');
    $('#divToolLuuQuyLai').css('display', 'none');
    $('#div2').css('display', 'none');
    Grid1.refresh();
}

function suaDuLieu(sttCV) {
    var array = getAjax('json', "../quanly/quanlycv.aspx/LayDuLieuTheoID", { cauTruyVan: sttCV });
    hienDiv2();
    TruocKhiThem();
    $("#TieuDeDiv2").text('Cập nhật thông tin CV');
    $('#txtHdMaCV').val(array[0].sttCV);
    $('#txtMaCC').val(array[0].maCC);
    $('#txtHoTen').val(array[0].hoTen);
    $('#txtTo').val(array[0].toLam);
    $('#txtNgayVaoLam').val(array[0].ngayVaoLam);
    $('#txtThamNien').val(array[0].thamNien);
    $('#txtSoDienThoai').val(array[0].soDienThoai);
    $('#txtNguoiGioiThieu').val(array[0].nguoiGioiThieu);
    $('#txtNgaySinh').val(array[0].ngaySinh);
    $('#txtGioiTinh').val(array[0].gioiTinh);
    $('#txtCMND').val(array[0].soCMND);
    $('#txtNgayCap').val(array[0].ngayCap);
    $('#txtNoiCap').val(array[0].noiCap);
    $('#txtQueQuan').val(array[0].nguyenQuan);
    $('#txtDanToc').val(array[0].danToc);
    $('#txtTonGiao').val(array[0].tonGiao);
    $('#txtChucDanh').val(array[0].chucDanh);
    $('#txtTrinhDoVanHoa').val(array[0].trinhDoVanHoa);
    $('#txtTinhTrangGiaDinh').val(array[0].trinhTrangGiaDinh);
    $('#txtDiaChiThuongTru').val(array[0].diaChiThuongTruSHK);
    $('#txtSoHoKhau').val(array[0].soSHK);
    $('#txtTenChuHo').val(array[0].tenChuHoSHK);
    $('#txtQuanHeChuHo').val(array[0].quanHeChuHoSHK);
    $('#txtGhiChu').val(array[0].ghiChu);
    $('#txtPhanXuong').val(array[0].phanXuong);
    $('#txtDiaChiThuongTruCMND').val(array[0].diaChiThuongTruCMND)
    $('#btnLuuVaThem').css('display', 'none');
    tempThem = false;
};

function TruocKhiThem() {
    $('#div2').find('input,textarea').val(null);
    $('#txtHoTen').focus();
    tempThem = true;
    $('#btnLuuVaThem').css('display', 'inline-block');
    $('#txtMaTrang').prop('disabled', false);
}

function xoaDuLieu(sttHD) {
    var cof = confirm('Bạn có chắc xóa CV này?');
    if (cof) {
        var result = getAjax('boolean', "../quanly/quanlyhopdong.aspx/XoaHD", { sttHD: sttHD });
        if (result) {
            tbsuccess('Xóa dữ liệu thành công.');
            layToanBoDuLieu();
        } else {
            tbdanger('Xóa dữ liệu không thành công. Vui lòng kiểm tra lại.');
        }
    }
}

function btnLuuDuLieu(sender, flag) {
    var saveData = new Array();
    saveData[0] = $('#txtMaCC').val();
    saveData[1] = $('#txtHoTen').val();
    saveData[2] = $('#txtTo').val();
    saveData[3] = $('#txtNgayVaoLam').val();
    saveData[4] = $('#txtThamNien').val();
    saveData[5] = $('#txtSoDienThoai').val();
    saveData[6] = $('#txtNguoiGioiThieu').val();
    saveData[7] = $('#txtNgaySinh').val();
    saveData[8] = $('#txtGioiTinh').val();
    saveData[9] = $('#txtCMND').val();
    saveData[10] = $('#txtNgayCap').val();
    saveData[11] = $('#txtNoiCap').val();
    saveData[12] = $('#txtQueQuan').val();
    saveData[13] = $('#txtDanToc').val();
    saveData[14] = $('#txtTonGiao').val();
    saveData[15] = $('#txtChucDanh').val();
    saveData[16] = ($('#txtTrinhDoVanHoa').val() == "" || $('#txtTrinhDoVanHoa').val().length == 0) ? "" : ($('#txtTrinhDoVanHoa').val() + "/12");
    saveData[17] = $('#txtTinhTrangGiaDinh').val();
    saveData[18] = $('#txtDiaChiThuongTru').val();
    saveData[19] = $('#txtSoHoKhau').val();
    saveData[20] = $('#txtTenChuHo').val();
    saveData[21] = $('#txtQuanHeChuHo').val();
    saveData[22] = $('#txtGhiChu').val();
    saveData[23] = $('#txtPhanXuong').val();
    saveData[25] = $('#txtDiaChiThuongTruCMND').val();
    saveData[24] = null;

    if (saveData[16].search('/') != -1) {
        saveData[16] = saveData[16].split('/')[0] + "/12";
    }

    if (saveData[1] == "") {
        tbdanger('Vui lòng nhập họ tên');
        $('#txtHoTen').focus();
        return;
    }
    if (!tempThem) {
        saveData[24] = $('#txtHdMaCV').val();
        var result = getAjax('boolean', '../quanly/quanlycv.aspx/SuaCV', { saveData: saveData });
        if (result) {
            tbsuccess('Cập nhật dữ liệu thành công.');
            var hienthi = $('#mdThemMoi');
            hienthi.modal('hide');
            hienDiv1();
            layToanBoDuLieu();
        } else {
            tbdanger('Cập nhật dữ liệu không thành công. Vui lòng kiểm tra lại.');
        }
    } else {
        var result = getAjax('string', '../quanly/quanlycv.aspx/ThemCV', { saveData: saveData });
        if (result == "ThanhCong") {
            if (flag == '1') {
                tbsuccess('Thêm mới dữ liệu thành công.');
                layToanBoDuLieu();
                hienDiv1();
            }
            else {
                TruocKhiThem();
                tbsuccess('Thêm mới dữ liệu thành công.');
                layToanBoDuLieu();
            }
        }
        else
            if (result == "TonTai")
                tbinfo('Dữ liệu này đã tồn t');
            else
                tbdanger('Cảnh báo ' + result);
    }
}

function XuatWordHD(){
    if($('#selNamThang').val()=="" || $('#selNamThang').val().length==0){
        tbdanger('Vui lòng chọn thời gian nhập');
        return !1;
    }
    tbinfo('Đang soạn Word, đợi trong giây lát');
    $('#linkWord').attr('href', '#');
    // xuất theo thời gian
    var result = getAjax('string', '../quanly/quanlyhopdong.aspx/XuatWordHD', { thoigian: $('#selNamThang').val() });
    tban();
    if (result=="KhongThanhCong") {
        tbdanger('Lỗi, không thể tạo hợp đồng vui lòng thử lại');
    }else{
        $('#mdKetQua').modal('show');
        $('#linkWord').attr('href', result);
    }
    return !1;
}
$(document).on('click', '#linkWord', function () {
    setTimeout(function () {
        $('#linkWord').attr('href', '#');
        $('#mdKetQua').modal('hide');
    }, 500);
    return;
});