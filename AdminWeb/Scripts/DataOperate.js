
function alert(msg) {
    layer.alert(msg);

}
 
function refresh()
{
    //刷新页面
    parent.window.location.href = parent.window.location.href;
    //获取窗口索引
    var index = parent.layer.getFrameIndex(window.name);
    //关闭弹出层
    parent.layer.close(index);
}

/*数据处理*/
function JsonToJson(data) {
    var reg = new RegExp('&quot;', 'g');
    data = data.replace(reg, '"');
    return JSON.parse('' + data + '');
}
function post(url, data) {
    var temp = document.createElement("form");
    temp.action = url;
    temp.method = "post";
    temp.style.display = "none";
    for (var x in data) {
        var opt = document.createElement("input");
        opt.name = x;
        opt.value = data[x] == null ? "" : data[x];
        temp.appendChild(opt);
    }
    document.body.appendChild(temp);
    temp.submit();
    return temp;
}