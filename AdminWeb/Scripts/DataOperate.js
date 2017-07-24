
function alert(msg,callback)
{
    layer.alert(msg,function(res){
        callback.call(this,res)
    });
  
}