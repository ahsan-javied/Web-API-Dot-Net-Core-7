import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { GlobalModel } from '../models/global.model';

@Injectable({
  providedIn: 'root'
})
export class GlobalService {

  public configuration: GlobalModel = new GlobalModel();

  constructor() { }

  async setConfig(): Promise<GlobalModel> {
    this.configuration.Server = environment.server;
    this.configuration.ApiUrl = environment.apiUrl;
    return this.configuration;
  }

  readConfig(): GlobalModel {
      return this.configuration;
  }
}
