//主页面加载完毕
$(document).ready(function () {
    //顶部导航点击
    $(".navbutton").click(function () {
        if (Logined()) {
            if ($(this).attr("data-west") != undefined) {
                SetWest($(this).attr("data-title"), $(this).attr("data-west"));
            }
            if ($(this).attr("data-center") != undefined) {
                SetCenter($(this).attr("data-center"));
            }
        }
        else {
            $.messager.alert("提示", "登录超时，请重新登录！", "error");
        }
    });
    //修改密码点击
    $("#btnChangePassword").click(function () {
        if (Logined()) {
            if ($(this).attr("data-center") != undefined) {
                SetCenter($(this).attr("data-center"));
            }
        }
        else {
            $.messager.alert("提示", "登录超时，请重新登录！", "error");
        }
    });
});

function Logined() {
    var rtb = false;
    $.ajax({ url: '/Admin/Administrator/Logined', type: 'post', async: false, dataType: 'json', success: function (rt) { rtb = rt; } });
    return rtb;
    //$.post('/Admin/Administrator/Logined', null, function (rt) { alert(rt); return rt; },'json');
}

//设置导航面板内容
function SetWest(title, url) {
    var _layout = $('#layout');
    var _west = _layout.layout('panel', 'west');
    if (_west != undefined) {
        _west.panel('setTitle', title);
        _west.panel('refresh', url);
    }
}
//设置主面板内容
function SetCenter(url) {
    var _layout = $('#layout');
    var _center = _layout.layout('panel', 'center');
    if (_center != undefined) {
        _center.panel('refresh', url);
    }
}

//左侧菜单点击
function WestMenu() {
    $(".westmenuitem").click(function () {
        if (Logined()) {
            var _link = $(this);
            SetCenter(_link.attr("href"));
        }
        else {
            $.messager.alert("提示", "登录超时，请重新登录！", "error");
        }
        return false;
    });
}

//显示错误信息。变量为错误消息列表
function ShowValidationMessage(validationMessage) {
    $("[data-valmsg-for]").text(""); 
    if (validationMessage == undefined) return; 
    $.each(validationMessage, function (name, message) {
        if ($("#" + name) != undefined) {
            if ($("[data-valmsg-for='" + name + "']") != undefined) $("[data-valmsg-for='" + name + "']").text(message);
        }
    });
}

//验证失败
function AuthenticationFailed(code) {
    if (code == 1) {
        $.messager.alert("验证失败", "未登录或登录已超时，请重新登录。", "", function () {
            location.href = '/Admin/Administrator/Login';
        });
    }
    else {
        $.messager.alert("验证失败", "权限验证失败。");
    }
}