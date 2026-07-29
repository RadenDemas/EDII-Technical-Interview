function reindexItems(containerSelector, prefix) {
    $(containerSelector).children('.item-row').each(function (index) {
        $(this).find('input, select, textarea').each(function () {
            var name = $(this).attr('name');
            if (name) {
                var newName = name.replace(new RegExp(prefix + '\\[\\d+\\]'), prefix + '[' + index + ']');
                $(this).attr('name', newName);
            }
            var id = $(this).attr('id');
            if (id) {
                var newId = id.replace(new RegExp(prefix + '_\\d+_'), prefix + '_' + index + '_');
                $(this).attr('id', newId);
            }
        });

        $(this).find('label').each(function () {
            var forAttr = $(this).attr('for');
            if (forAttr) {
                var newForAttr = forAttr.replace(new RegExp(prefix + '_\\d+_'), prefix + '_' + index + '_');
                $(this).attr('for', newForAttr);
            }
        });
    });
}

function tambahItem(buttonId, containerId, templateId, prefix) {
    $(buttonId).click(function () {
        let index = $(containerId).children('.item-row').length;

        let html = $(templateId).html();
        html = html.replace(/__INDEX__/g, index);

        $(containerId).append(html);
    });
}

$(document).on("click", ".btn-hapus", function () {
    let container = $(this).closest('.item-container');
    let containerId = container.attr('id');
    
    $(this).closest(".item-row").remove();
    
    if (containerId === 'pendidikan-container') {
        reindexItems('#pendidikan-container', 'Pendidikan');
    } else if (containerId === 'pelatihan-container') {
        reindexItems('#pelatihan-container', 'RiwayatPelatihan');
    } else if (containerId === 'pekerjaan-container') {
        reindexItems('#pekerjaan-container', 'RiwayatPekerjaan');
    }
});

$(function () {
    tambahItem("#btnTambahPendidikan", "#pendidikan-container", "#templatePendidikan", "Pendidikan");
    tambahItem("#btnTambahPelatihan", "#pelatihan-container", "#templatePelatihan", "RiwayatPelatihan");
    tambahItem("#btnTambahPekerjaan", "#pekerjaan-container", "#templatePekerjaan", "RiwayatPekerjaan");
});

// Perbaiki bug validasi jQuery untuk angka desimal dengan koma (Culture id-ID)
if ($.validator) {
    $.validator.methods.range = function (value, element, param) {
        var globalizedValue = value.replace(",", ".");
        return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
    }
    $.validator.methods.number = function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
    }
}
