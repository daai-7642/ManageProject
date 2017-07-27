
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