import { Injectable } from '@angular/core';
import { DeviceDetectorService } from 'ngx-device-detector';
import { environment } from 'src/environments/environment';
import { GlobalModel } from '../../models/global.model';
import { CustomHttpClientService } from './customHttpClient.service';

@Injectable({
  providedIn: 'root'
})
export class GlobalService {

  public configuration: GlobalModel = new GlobalModel();

  constructor(private customHttpClientService: CustomHttpClientService, private deviceService: DeviceDetectorService) { }

  async setConfig(): Promise<GlobalModel> {
    this.configuration.Server = environment.server;
    this.configuration.ApiUrl = environment.apiUrl;
    this.configuration.Browser = this.deviceService.browser;
    this.configuration.Device = this.deviceService.device;

    this.getIPAddress()
    .then((data) => {
      if (data) {
        this.configuration.IpAddress = data;
      }
    });

    return this.configuration;
  }

  readConfig(): GlobalModel {
      return this.configuration;
  }

  async getIPAddress(): Promise<string> {
    return new Promise(resolve => {
      this.customHttpClientService.getPublicIpAddress()
        .subscribe((data) => {
          if (data) {
            resolve(data.query);
          }
          else {
            resolve('');
          }
        });
    });
  }

}
