//Add视图添加载完成函数
function CategoryAddReady() {
    $.post($("#ParentId").attr("data-url"), null, function (data) {
        if (data == "") data = [{ id: 0, text: "无" }];
        else {
        }
        $("#ParentId").combotree('loadData', data);

    });
    $("#Type").combobox({
        data:[{id:0,text:"sssd" }],
        required: true,
        onSelect: function (rec) { CategoryAdd_TypeChange(rec); }
    });
    $("#CategoryView").validatebox({ required: true });
    $("#ContentView").validatebox({ required: true });
    $("#LinkUrl").validatebox({ required: true });
    $("#ContentOrder").validatebox({ required: true });
    $("#PageSize").validatebox({ required: true });
    $("#RecordUnit").validatebox({ required: true });
    $("#RecordName").validatebox({ required: true });
    //保存事件
    $("#CategoryAdd_Save").click(function () {
        CategoryAdd_Save();
    });
}
//Add视图类型切换事件
function CategoryAdd_TypeChange(rec) {
    //常规栏目
    if (rec.value == 0) {
        //模型
        $("#Model").parent().parent().show();
        //栏目视图
        $("#CategoryView").validatebox("enableValidation");
        $("#CategoryView").parent().parent().show();
        //内容视图
        $("#ContentView").validatebox("enableValidation");
        $("#ContentView").parent().parent().show();
        //链接地址
        $("#LinkUrl").parent().parent().hide();
        $("#LinkUrl").validatebox("disableValidation");
        //内容排序
        $("#ContentOrder").parent().parent().show();
        $("#ContentOrder").validatebox("enableValidation");
        //每页记录数
        $("#PageSize").parent().parent().show();
        $("#PageSize").validatebox("enableValidation");
        //记录单位
        $("#RecordUnit").parent().parent().show();
        $("#RecordUnit").validatebox("enableValidation");
        //记录名称
        $("#RecordName").parent().parent().show();
        $("#RecordName").validatebox("enableValidation");
    }//单页栏目
    else if (rec.value == 1) {
        //模型
        $("#Model").parent().parent().hide();
        //栏目视图
        $("#CategoryView").validatebox("enableValidation");
        $("#CategoryView").parent().parent().show();
        //内容视图
        $("#ContentView").validatebox("enableValidation");
        $("#ContentView").parent().parent().show();
        //链接地址
        $("#LinkUrl").parent().parent().hide();
        $("#LinkUrl").validatebox("disableValidation");
        //内容排序
        $("#ContentOrder").parent().parent().hide();
        $("#ContentOrder").validatebox("disableValidation");
        //每页记录数
        $("#PageSize").parent().parent().hide();
        $("#PageSize").validatebox("disableValidation");
        //记录单位
        $("#RecordUnit").parent().parent().hide();
        $("#RecordUnit").validatebox("disableValidation");
        //记录名称
        $("#RecordName").parent().parent().hide();
        $("#RecordName").validatebox("disableValidation");
    }//外部链接
    else if (rec.value == 2) {
        //模型
        $("#Model").parent().parent().hide();
        //栏目视图
        $("#CategoryView").validatebox("disableValidation");
        $("#CategoryView").parent().parent().hide();
        //内容视图
        $("#ContentView").validatebox("disableValidation");
        $("#ContentView").parent().parent().hide();
        //链接地址
        $("#LinkUrl").parent().parent().show();
        $("#LinkUrl").validatebox("enableValidation");
        //内容排序
        $("#ContentOrder").parent().parent().hide();
        $("#ContentOrder").validatebox("disableValidation");
        //每页记录数
        $("#PageSize").parent().parent().hide();
        $("#PageSize").validatebox("disableValidation");
        //记录单位
        $("#RecordUnit").parent().parent().hide();
        $("#RecordUnit").validatebox("disableValidation");
        //记录名称
        $("#RecordName").parent().parent().hide();
        $("#RecordName").validatebox("disableValidation");
    }
}
//添加保存
function CategoryAdd_Save() {
    $('#categoryadd_form').form('submit', {
        success: function (data) {
            var rt = jQuery.parseJSON(data);
            //验证成功
            if (rt.Authentication == 0) {
                //操作成功
                if (rt.Success) {
                    $.messager.alert("添加栏目成功", rt.Message, "", function () {
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
                    $.messager.alert("添加栏目失败", msg, "error");
                }
            }
            else if (rt.Authentication == 1) {
                $.messager.alert("验证失败", "未登录或登录已超时，请重新登录。", "", function () {
                    location.href("Admin/Administrator/Login");
                });
            }
        }
    });
}