//登录
function AdminLogin(url) {
    $.post($('form').attr('action'), $('form').serializeArray(), function (rt) {
        if (rt.Success) location.href = url;
        else {
            $.messager.alert("登录失败", rt.Message, "error");
        }
    }
        , 'json');
    return false;
}

//显示添加对话框
function AdminAddDlgShow()
{
    $("#adminadddlg").dialog("open");
    $("#adminadddlg").dialog("refresh");
}

//添加管理员保存
function AdminAddSave() {
    
    $('#adminadd_form').form('submit', {
        success: function (data) {
            var rt = jQuery.parseJSON(data);
            if (rt.Success) {
                $.messager.alert("保存成功", rt.Message,"" ,function () {
                    $("#adminadddlg").dialog("close");
                    $('#admin_datagrid').datagrid("reload");
                });
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

//关闭添加管理员窗口
function AdminAddDlgClose() {
    $("#adminadddlg").dialog("close");
}

//删除管理员
function AdminDelRow(url) {
    var row = $('#admin_datagrid').datagrid('getSelected');
    if (row) {
        $.messager.confirm('确认', '你确定要删除此管理员', function (r) {
            if (r) {
                $.post(url, { Id: row.AdministratorId }, function (data) {
                    if (data.Success) {
                        $.messager.alert("删除成功", data.Message, "", function () { $("#admin_datagrid").datagrid("reload"); });
                    }
                    else $.messager.alert("错误", data.Message, "error");
                });
            }
        });
    }
}

//显示修改密码窗口
function ShowChangePwdDlg(url) {
    $(document.body).append("<div id='cPwdDlg'></div>");
    $('#cPwdDlg').dialog({
        title: "修改密码",
        width: 480,
        height: 260,
        closed: false,
        cache: false,
        href: url,
        modal: true,
        onClose: function () { $(this).dialog("destroy"); }
    });
}

//修改密码保存
function AdminCPwdSave() {
    $('#admincha_form').form('submit', {
        success: function (data) {
            var rt = jQuery.parseJSON(data);
            if (rt.Success) {
                $.messager.alert("保存成功", rt.Message, "", function () {
                    location.href = $("#btn_Logout").attr("href");
                    $("#cPwdDlg").dialog("destroy");
                    
                });
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