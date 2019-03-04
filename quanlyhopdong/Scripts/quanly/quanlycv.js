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

$("#txtDanToc").autocomplete({
    source: function (request, response) {
        var data = getAjax('json', '../quanly/quanlycv.aspx/LayDanToc', { key: $('#txtDanToc').val() });
        response($.map(data, function (item) {
            return {
                label: item.danToc,
                value: item.danToc
            }
        }));
    },
    minlength: 1,
    select: function (event, ui) {

    }
});

$("#txtTonGiao").autocomplete({
    source: function (request, response) {
        var data = getAjax('json', '../quanly/quanlycv.aspx/LayTonGiao', { key: $('#txtTonGiao').val() });
        response($.map(data, function (item) {
            return {
                label: item.tonGiao,
                value: item.tonGiao
            }
        }));
    },
    minlength: 1,
    select: function (event, ui) {

    }
});


$("#txtTenChuHo").autocomplete({
    source: function (request, response) {
        var data = getAjax('json', '../quanly/quanlycv.aspx/LayTenChuHo', { key: $('#txtTenChuHo').val() });
        response($.map(data, function (item) {
            return {
                label: item.tenChuHoSHK,
                value: item.tenChuHoSHK
            }
        }));
    },
    minlength: 1,
    select: function (event, ui) {

    }
});


$("#txtQuanHeChuHo").autocomplete({
    source: function (request, response) {
        var data = getAjax('json', '../quanly/quanlycv.aspx/LayQuanHeChuHo', { key: $('#txtQuanHeChuHo').val() });
        response($.map(data, function (item) {
            return {
                label: item.quanHeChuHoSHK,
                value: item.quanHeChuHoSHK
            }
        }));
    },
    minlength: 1,
    select: function (event, ui) {

    }
});
$("#txtHoTen").autocomplete({
    source: function (request, response) {
        var data = getAjax('json', '../quanly/quanlycv.aspx/LayTheoHoTen', { key: $('#txtHoTen').val() });
        response($.map(data, function (item) {
            return {
                label: item.hoTen,
                value: item.sttCV
            }
        }));
    },
    minlength: 1,
    select: function (event, ui) {
        suaDuLieu(ui.item.value);
    }
});

$("#txtTinhTrangGiaDinh").autocomplete({
    source: ["Độc thân", "1 con", "2 con", "3 con"],
    minlength: 1
});
$(function () {
    layToanBoDuLieu();
    $('#txtTuNgay,#txtDenNgay').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yyyy'
    });
    $('#meCV').addClass('active');
});
var dulieu;
function layToanBoDuLieu() {
    $("#Loadding").show();
    Grid1.refresh();
    $('#divload').show();
    $("#Loadding").hide();
}

// thêm mới
$('#btnThem').click(function () {
        $("#TieuDeDiv2").text('Thêm mới CV');
        $('#div1').css('display', 'none');
        $('#divToolbarThemMoi').css('display', 'none');
        $('#divToolLuuQuyLai').css('display', 'block');
        $('#div2').css('display', 'block');
        TruocKhiThem();
});
// quay lại
$('#btnQuayLai').click(function () {
    var check = !0, check2=!1;
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

$(document).on('click', '#btnLamLai', () => {
    var cof = confirm('Bạn có chắc làm lại CV? Tất cả các ô nhập sẽ đặt là rỗng.');
    if(cof)
        $('#khungNhapCV').find('input, textarea').val(null);
    return !1;
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

function xoaDuLieu(sttCV) {
    var cof = confirm('Bạn có chắc xóa CV này?');
    if (cof) {
        var result = getAjax('boolean', "../quanly/quanlycv.aspx/XoaCV", { sttCV: sttCV });
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
    saveData[16] = ($('#txtTrinhDoVanHoa').val() == "" || $('#txtTrinhDoVanHoa').val().length==0) ? "" : ($('#txtTrinhDoVanHoa').val() + "/12");
    saveData[17] = $('#txtTinhTrangGiaDinh').val();
    saveData[18] = $('#txtDiaChiThuongTru').val();
    saveData[19] = $('#txtSoHoKhau').val();
    saveData[20] = $('#txtTenChuHo').val();
    saveData[21] = $('#txtQuanHeChuHo').val();
    saveData[22] = $('#txtGhiChu').val();
    saveData[23] = $('#txtPhanXuong').val();
    saveData[25] = $('#txtDiaChiThuongTruCMND').val();
    saveData[24] = null;

    if (saveData[16].search('/')!=-1) {
        saveData[16] = saveData[16].split('/')[0]+"/12";
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
function XuaExcelCV(loai) {
    
    var tu = $('#txtTuNgay').val();
    var den = $('#txtDenNgay').val();
    if ((loai == '1') && (tu == '' || den == '')) {
        tbdanger('Vui lòng chọn khoảng thời gian xuất excel');
        return;
    }
    var data = getAjax('string', '../quanly/quanlycv.aspx/XuaExcelCV', {tuNgay: $('#txtTuNgay').val(), denNgay: $('#txtDenNgay').val(), loai: loai});
    if (data!=null) {
        $('#linkExcel').attr('href', data);
        $('#mdKetQua').modal('show');
    }
    return;
}
$(document).on('click', '#linkExcel', function () {
    setTimeout(function () {
        $('#linkExcel').attr('href', '#');
        $('#mdKetQua').modal('hide');
    }, 500);
    return;
});