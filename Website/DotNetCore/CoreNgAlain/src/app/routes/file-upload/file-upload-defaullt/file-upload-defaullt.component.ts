import { Component, OnInit } from '@angular/core';
import { UploadFile, NzNotificationService } from 'ng-zorro-antd';
// import { Config } from '@assets/Config';

@Component({
  selector: 'app-file-upload-defaullt',
  templateUrl: './file-upload-defaullt.component.html',
})
export class FileUploadDefaulltComponent implements OnInit {
    //图片上传
    //图片上传接口
    // actionUrl=this.config.apiHost+"/FileUpload/UploadFile";
    actionUrl="http://localhost:61541/FileUpload/UploadFile";
    showUploadList = {
      showPreviewIcon: true,
      showRemoveIcon: true,
      hidePreviewIconInNonImage: true
    };
    fileList : UploadFile[];//用于前端判定
    previewImage: string | undefined = '';
    previewVisible = false;

  constructor(
    // private config:Config,
    private notification: NzNotificationService
  ) { }

  ngOnInit() {

  }

  // //图片预览
  // handlePreview = (file: UploadFile) => {
  //   this.previewImage = file.url || file.thumbUrl;
  //   this.previewVisible = true;
  // };

  // //图片上传前
  // beforeUpload = async (item) => {
  //   try {
  //     //这里赋值参数item无意义，没有改变实际上传的图片对象，所以不能在这里完成图片压缩
  //     return true;
  //   } catch (e) {
  //       return false;
  //   }
  // }

  // //图片上传返回
  // handleChange(info: { file: UploadFile }): void {
  //   if (info.file.status === 'error') {
  //     this.notification.create('error','error','上传图片异常，请重试');
  //   }
  //   if (info.file.status === 'done') {
  //     this.getBase64(info.file.originFileObj, (img: any) => {
  //       //获得图片的base64
  //     });
  //     this.notification.create('success','success','上传图片完成');
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
  //   return true;
  // }
  // private findImgIndex(url) {
  //   let retIndex = -1;
  //   this.fileList.forEach((item, index) => {
  //       if (item.thumbUrl === url || item.url === url) retIndex = index;
  //   });
  //   return retIndex;
  // }

  // getFileList=function(){
  //   console.log(this.fileList);
  // }
}
