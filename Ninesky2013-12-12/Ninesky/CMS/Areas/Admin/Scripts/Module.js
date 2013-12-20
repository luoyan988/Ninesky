
function ModuleBase_Ready() {
    $("#btn_modulebase_save").click(function () {
        ModuleBase_Save();
    });
}
function ModuleBase_Save() {
    $.post($('#module_base').attr('action'), $('#module_base').serializeArray(), function (rt) {
        if (rt.Authentication == 0) {
            if (rt.Success) {
                $.messager.alert("修改模块信息成功", rt.Message, "info");
            }
            else {
                if (rt.ValidationList != undefined) ShowValidationMessage(rt.ValidationList);
                $.messager.alert("修改模块信息失败", rt.Message, "error");
            }
        }
        else {
            AuthenticationFailed(rt.Authentication);
        }
    }, 'json');
}