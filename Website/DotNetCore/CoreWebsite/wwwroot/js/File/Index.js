$(function () {
    //资料:http://blog.csdn.net/qq_23217629/article/details/52221364
    $("#file_upload_1").uploadify({
        height: 30,
        width: 120,
        swf: '/uploadify/uploadify.swf',
        //uploader: '/uploadify/uploadify.php',
        uploader: '/QiNiuFileUpload/UploadFileTest',
        fileObjName: "uploadFile",  // 控制器中参数名称  
        fileSizeLimit: "1024KB",   //文件大小限制
        fileTypeExts: "*.jpg;*.gif;*.png;",    //文件格式
        multi: false,//设置值为false时，一次只能选中一个文件。
        auto: true,//设置auto为true，当文件被添加至上传队列时，将会自动上传。
        buttonText: '上传',//定义显示在默认按钮上的文本。
        onUploadSuccess: function (file, result, response) {   //上传成功
            if (result) {
                //图片预览 设置图片路径  
                $("#img").attr("src", result);
            }
            // 上传失败  
        }  
    });
});