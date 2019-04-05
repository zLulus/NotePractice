import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-file-upload-for-custom-request',
  templateUrl: './file-upload-for-custom-request.component.html',
})
export class FileUploadForCustomRequestComponent implements OnInit {
// //图片上传
//     //图片上传接口
//     actionUrl=AppConsts.remoteServiceBaseUrl+"/FileUpload/UploadFile";
//     showUploadList = {
//       showPreviewIcon: true,
//       showRemoveIcon: true,
//       hidePreviewIconInNonImage: true
//     };
//     fileList : UploadFile[];//用于前端判定
//     previewImage: string | undefined = '';
//     previewVisible = false;

  constructor(
  ) { }

  ngOnInit() {

  }

  //图片预览
  handlePreview = (file: UploadFile) => {
    this.previewImage = file.url || file.thumbUrl;
    this.previewVisible = true;
  };

  // // 自定义上传方法的实现
  // uploadImp = async (item) => {
  //   try {
  //       // 图片缩略
  //       const compressionFile = await new ImageCompressor().compress(item.file, {
  //         quality: .8,
  //         maxWidth: 128,
  //         maxHeight: 128
  //       });
  //       item.file=compressionFile;
        

  //       if (compressionFile.size > 0.5 * 1024 * 1024) throw {message: '压缩后的图片超过了0.5M'}

  //       //这里是请求后端接口的写法
  //       const formData = new FormData();
  //       formData.append(item.name, compressionFile,item.file.name);
  //       const url = item.action;
  //       const req = new HttpRequest('POST', url, formData, { reportProgress: true });
  //       this.http.request(req).pipe(filter(e => e instanceof HttpResponse)).subscribe((event: {}) => {
  //         //https://ng.ant.design/components/upload/zh
  //         // item.file.response = event['body'];
  //       //   {
  //       //     uid: 'uid',      // 文件唯一标识
  //       //     name: 'xx.png'   // 文件名
  //       //     status: 'done', // 状态有：uploading done error removed
  //       //     response: '{"status": "success"}', // 服务端响应内容
  //       //     linkProps: '{"download": "image"}', // 下载链接额外的 HTML 属性
  //       //  }
  //       //onSuccess: (body: Object, xhr?: Object): void
  //           //这里把后端方法返回结果取出
  //         item.onSuccess(event['body'], {
  //           status: 'done'
  //         });
  //       }, err => this.notify.error(`图片上传失败，请重试`));
  //   } catch (e) {
  //       const msg = e.message ? e.message : e;
  //       this.notify.error(`图片上传失败，请重试：${msg}`);
  //       item.onError(e);
  //   }
  // }

  // //图片上传返回
  // handleChange(info: { file: UploadFile }): void {
  //   if (info.file.status === 'error') {
  //     this.notify.error('上传图片异常，请重试');
  //   }
  //   if (info.file.status === 'done') {
  //     this.getBase64(info.file.originFileObj, (img: any) => {
  //       //获得图片的base64
  //     });
  //     //这里只取了1个Url
  //     this.data.iconUrl=info.file.response.result.imageUrl;
  //     this.notify.success('上传图片完成');
  //   }
  // }
  // private getBase64(img: File, callback: (img: any) => void) {
  //   const reader = new FileReader();
  //   reader.addEventListener('load', () => callback(reader.result));
  //   reader.readAsDataURL(img);
  // }

  // //删除图片
  // onRemove = (file: UploadFile) => {
  //   const currIndex = this.findImgIndex(file);
  //   delete this.fileList[currIndex];
  //   //清空
  //   this.data.iconUrl="";
  //   return true;
  // }
  // private findImgIndex(url) {
  //   let retIndex = -1;
  //   this.fileList.forEach((item, index) => {
  //       if (item.thumbUrl === url || item.url === url) retIndex = index;
  //   });
  //   return retIndex;
  // }
}
