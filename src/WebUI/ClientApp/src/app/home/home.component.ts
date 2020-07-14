import { Component, ViewChild, ElementRef, Output, EventEmitter, OnInit } from '@angular/core';
import { FileClient, FileParameter, ImportedFile } from '../alpvisionapp-api';

// export interface FileParameter {
//   data: any;
//   // fileName: string;
//   // size: number;
//   // type: string;
// }
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit{
@ViewChild("file", {static: false}) fileUpload: ElementRef;
files  = [];
importedFilesTable : ImportedFile[] = [];


  constructor(private fileClient: FileClient){}
  ngOnInit(){
    //Get all the imported files informations from the database
    // this.fileClient.get().subscribe(
    //   (result) => {
    //     console.log(result);
    //     this.importedFilesTable = result;
    //   }
    // )
  }

  uploadFile()
  {
    console.log("UPLOAD FILE FUNCTON");
    if (this.fileUpload.nativeElement.files.length === 0) {
      this.fileUpload.nativeElement.click();
      return;
    }
    const fileToUpload = <File>this.fileUpload.nativeElement.files[0];
    var formFile = <FileParameter>{data: fileToUpload, fileName : fileToUpload.name}
    this.fileClient.import(formFile)
      .subscribe(result => {
        console.log(result);
        let dataType = result.data.type;
        let binaryData = [];
        binaryData.push(result.data);
        let downloadLink = document.createElement('a');
        downloadLink.href = window.URL.createObjectURL(new Blob(binaryData, { type: dataType }));
        if (result.fileName)
          downloadLink.setAttribute('download', result.fileName);
        document.body.appendChild(downloadLink);
        downloadLink.click();
        });
  }


}
