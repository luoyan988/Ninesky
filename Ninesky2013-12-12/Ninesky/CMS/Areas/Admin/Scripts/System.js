//Config页提交
function ConfigSubmit() {
    $('#siteconfig_form').form('submit', {
        success: function (data) {
            var rt = jQuery.parseJSON(data);
            if (rt.Success) {
                $.messager.alert("保存成功", rt.Message);
            }
            else {
                var msg = "";
                if (rt.MessageLsit != undefined) {
                    $.each(rt.MessageLsit, function (i, val) {
                        msg += "<li>" + i + ":" + val + "</li>";
                    });
                }
                if (msg != "") msg = rt.Message + "<br /> <p> 原因如下：" + "<ul>" + msg + "</ul></p>";
                else msg = rt.Message;
                $.messager.alert("保存失败", msg, "error");
            }
        }
    });
}