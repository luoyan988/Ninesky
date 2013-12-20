//Add视图添加载完成函数
function CategoryAddReady() {
    $.post($('#ParentId').attr('data-url'), null, function (data) {
        if (data == "") data = [{ id: 0, text: "无" }];
        else {
            data.unshift({ id: 0, text: "无" });
        }
        $('#ParentId').combotree({ required: true, data: data });

    });
    $('#Type').combobox({
        valueField: 'id',
        textField: 'text',
        data: [{ 'id': 0, 'text': '常规栏目' }, { 'id': 1, 'text': '单页栏目' }, { 'id': 2, 'text': '外部链接' }],
        required: true,
        onSelect: function (rec) {
            CategoryAdd_TypeChange(rec.id);
        }
    });
    $.post($('#Model').attr("data-url"), null, function (data) {
        if (data == "") data = [{ Model: "", Name: "无" }];
        else {
            data.unshift({ Model: "", Name: "无" });
        }
        $('#Model').combobox({
            valueField: 'Model',
            textField: 'Name',
            data: data,
            required: true,
            onSelect: function (rec) {
                CategoryAdd_ModelChange(rec.Model);
            }
        });
    });

    $('#ContentView').parent().parent().hide();
    $('#LinkUrl').parent().parent().hide();
    $('#ContentOrder').parent().parent().hide();
    $('#RecordUnit').parent().parent().hide();
    $('#PageSize').parent().parent().hide();
    $('#RecordName').parent().parent().hide();
    //保存事件
    $('#CategoryAdd_Save').click(function () {
        CategoryAdd_Save();
    });
    //CategoryAdd_TypeChange(0);
}
//Add视图类型切换事件
function CategoryAdd_TypeChange(typeId) {
    //常规栏目
    if (typeId == 0) {
        //模型
        $('#Model').parent().parent().show();
        var _modelValue = $('#Model').combobox('getValue');
        CategoryAdd_ModelChange(_modelValue);

    }//单页栏目
    else if (typeId == 1) {
        //模型
        $('#Model').parent().parent().hide();
        //栏目视图
        $('#CategoryView').parent().parent().show();
        //内容视图
        $('#ContentView').parent().parent().hide();
        //链接地址
        $('#LinkUrl').parent().parent().hide();
        //内容排序
        $('#ContentOrder').parent().parent().hide();
        //每页记录数
        $('#PageSize').parent().parent().hide();
        //记录单位
        $('#RecordUnit').parent().parent().hide();
        //记录名称
        $('#RecordName').parent().parent().hide();
    }//外部链接
    else if (typeId == 2) {
        //模型
        $('#Model').parent().parent().hide();
        //栏目视图
        $('#CategoryView').parent().parent().hide();
        //内容视图
        $('#ContentView').parent().parent().hide();
        //链接地址
        $('#LinkUrl').parent().parent().show();
        //内容排序
        $('#ContentOrder').parent().parent().hide();
        //每页记录数
        $('#PageSize').parent().parent().hide();
        //记录单位
        $('#RecordUnit').parent().parent().hide();
        //记录名称
        $('#RecordName').parent().parent().hide();
    }
}

//Add视图模型切换事件
function CategoryAdd_ModelChange(value) {
    if (value == "")//模型为无
    {
        //栏目视图

        $('#CategoryView').parent().parent().show();
        //内容视图
        $('#ContentView').parent().parent().hide();
        //链接地址
        $('#LinkUrl').parent().parent().hide();
        //内容排序
        $('#ContentOrder').parent().parent().hide();
        //每页记录数
        $('#PageSize').parent().parent().hide();
        //记录单位
        $('#RecordUnit').parent().parent().hide();
        //记录名称
        $('#RecordName').parent().parent().hide();
    }
    else {
        $('#CategoryView').parent().parent().show();
        //内容视图
        $('#ContentView').parent().parent().show();
        //链接地址
        $('#LinkUrl').parent().parent().hide();
        //内容排序
        $('#ContentOrder').parent().parent().show();
        //每页记录数
        $('#PageSize').parent().parent().show();
        //记录单位
        $('#RecordUnit').parent().parent().show();
        //记录名称
        $('#RecordName').parent().parent().show();
        var _modelValue = $('#Model').combobox('getValue');
        $('#ContentView').combobox('reload', $('#ContentView').attr('data-url') + '?controllerName=' + _modelValue);
    }
}
//添加保存
function CategoryAdd_Save() {
    $.post($('form').attr('action'), $('form').serializeArray(), function (rt) {
        if (rt.Authentication == 0) {
            if (rt.Success) {
                $(document.body).append("<div id='CategoryAdd_SuccessDialog'></div>");
                $('#CategoryAdd_SuccessDialog').dialog({
                    title: '操作成功',
                    width: 280,
                    height: 138,
                    closed: false,
                    cache: false,
                    content: '<br />添加栏目成功',
                    modal: true,
                    buttons: [{
                        text: '继续添加栏目',
                        handler: function () {
                            if ($('#categoryTreeView') != undefined) {
                                $('#categoryTreeView').tree('reload');
                            }
                            var _layout = $('#layout');
                            var _center = _layout.layout('panel', 'center');
                            _center.panel('refresh');
                            $('#CategoryAdd_SuccessDialog').dialog('destroy');
                        }
                    }, {
                        text: '关闭',
                        handler: function () {
                            if ($('#categoryTreeView') != undefined) {
                                $('#categoryTreeView').tree('reload');
                            }
                            $('#CategoryAdd_SuccessDialog').dialog('destroy');
                        }
                    }]
                });
            }
            else {
                if (rt.ValidationList != undefined) ShowValidationMessage(rt.ValidationList);
                $.messager.alert("添加栏目失败", rt.Message, "error");
            }
        }
        else {
            AuthenticationFailed(rt.Authentication);
        }
    }, 'json');
}
//栏目菜单加载完毕函数
function CategoryMenu_Ready() {
    $('#categoryTreeView').tree({
        url: $('#categoryTreeView').attr('data-url'),
        lines:true,
        onClick: function (node) {
            var _layout = $('#layout');
            var _center = _layout.layout('panel', 'center');
            _center.panel('refresh','/Admin/Category/Modify/' + node.id);
        }
    });
}

//修改栏目视图加载完毕函数
function CategoryModify_Ready() {
    $.post($('#ParentId').attr('data-url'), null, function (data) {
        if (data == "") data = [{ id: 0, text: "无" }];
        else {
            data.unshift({ id: 0, text: "无" });
        } 
        $('#ParentId').combotree({ data: data });

    });

    $('#Type').combobox({
        valueField: 'id',
        textField:'text',
        data: [{ 'id': 0, 'text': '常规栏目' }, { 'id': 1, 'text': '单页栏目' }, { 'id': 2, 'text': '外部链接' }],
        onSelect: function (rec) {
            CategoryModify_TypeChange(rec.id);
        }
    });
    $.post($('#Model').attr("data-url"), null, function (data) {
        if (data == "") data = [{ Model: "", Name: "无" }];
        else {
            data.unshift({ Model: "", Name: "无" });
        }
        $('#Model').combobox({
            valueField: 'Model',
            textField: 'Name',
            data: data,
            onSelect: function (rec) {
                CategoryModify_ModelChange(rec.Model);
            }
        });
    });
    //保存事件
    $('#CategoryModify_Save').click(function () {
        CategoryModify_Save();
    });
    var _typeValue;
    if ($('#Model').css("display") == "none") _typeValue = $('#Type').combobox('getValue');
    else _typeValue = $('#Type').val();
    CategoryModify_TypeChange(_typeValue);
}

//Modify视图类型切换事件
function CategoryModify_TypeChange(typeId) {
    //常规栏目
    if (typeId == 0) {
        //模型
        $('#Model').parent().parent().show();
        var _modelValue;
        if ($('#Model').css("display") == "none") _modelValue = $('#Model').combobox('getValue');
        else _modelValue = $('#Model').val();
        CategoryModify_ModelChange(_modelValue);
    }//单页栏目
    else if (typeId == 1) {
        //模型
        $('#Model').parent().parent().hide();
        //栏目视图
        $('#CategoryView').parent().parent().show();
        //内容视图
        $('#ContentView').parent().parent().hide();
        //链接地址
        $('#LinkUrl').parent().parent().hide();
        //内容排序
        $('#ContentOrder').parent().parent().hide();
        //每页记录数
        $('#PageSize').parent().parent().hide();
        //记录单位
        $('#RecordUnit').parent().parent().hide();
        //记录名称
        $('#RecordName').parent().parent().hide();
    }//外部链接
    else if (typeId == 2) {
        //模型
        $('#Model').parent().parent().hide();
        //栏目视图
        $('#CategoryView').parent().parent().hide();
        //内容视图
        $('#ContentView').parent().parent().hide();
        //链接地址
        $('#LinkUrl').parent().parent().show();
        //内容排序
        $('#ContentOrder').parent().parent().hide();
        //每页记录数
        $('#PageSize').parent().parent().hide();
        //记录单位
        $('#RecordUnit').parent().parent().hide();
        //记录名称
        $('#RecordName').parent().parent().hide();
    }
}

//Modify视图模型切换事件
function CategoryModify_ModelChange(value) {
    if (value == "")//模型为无
    {
        //栏目视图
        $('#CategoryView').parent().parent().show();
        //内容视图
        $('#ContentView').parent().parent().hide();
        //链接地址
        $('#LinkUrl').parent().parent().hide();
        //内容排序
        $('#ContentOrder').parent().parent().hide();
        //每页记录数
        $('#PageSize').parent().parent().hide();
        //记录单位
        $('#RecordUnit').parent().parent().hide();
        //记录名称
        $('#RecordName').parent().parent().hide();
    }
    else {
        $('#CategoryView').parent().parent().show();
        //内容视图
        $('#ContentView').parent().parent().show();
        //链接地址
        $('#LinkUrl').parent().parent().hide();
        //内容排序
        $('#ContentOrder').parent().parent().show();
        //每页记录数
        $('#PageSize').parent().parent().show();
        //记录单位
        $('#RecordUnit').parent().parent().show();
        //记录名称
        $('#RecordName').parent().parent().show();
        var _modelValue;
        if ($('#Model').css("display") == "none") _modelValue = $('#Model').combobox('getValue');
        else _modelValue = $('#Model').val();
        var url = $('#ContentView').attr('data-url') + '?controllerName=' + _modelValue;
        $.post(url, null, function (data) {
           $('#ContentView').combobox('loadData', data);
        });
    }
}

//修改保存
function CategoryModify_Save() {
    $.post($('form').attr('action'), $('form').serializeArray(), function (rt) {
        if (rt.Authentication == 0) {
            if (rt.Success) {
                $("[data-valmsg-for]").text("");
                $.messager.alert("修改栏目成功", rt.Message, "info");
                if ($('#categoryTreeView') != undefined) {
                    $('#categoryTreeView').tree('reload');
                }
            }
            else {
                if (rt.ValidationList != undefined) ShowValidationMessage(rt.ValidationList);
                $.messager.alert("添加栏目失败", rt.Message, "error");
            }
        }
        else {
            AuthenticationFailed(rt.Authentication);
        }
    }, 'json');
}

//删除栏目
function CategoryDel(url, id) {
    if (confirm("你确定要删除此栏目吗？")) {
        $.post(url, { Id: id }, function (data) {
            //验证
            if (data.Authentication == 0) {
                //操作成功
                if (data.Success) {
                    $.messager.alert("删除栏目成功", data.Message, "info");
                    if ($('#categoryTreeView') != undefined) {
                        $('#categoryTreeView').tree('reload');
                    }
                }
                else {
                    $.messager.alert("删除栏目失败", data.Message, "error");
                }
            }
            else AuthenticationFailed(data.Authentication);
        }, "json");
    }
}
