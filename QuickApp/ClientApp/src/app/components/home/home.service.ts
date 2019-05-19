// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { Injectable, Injector } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';

import { ConfigurationService } from '../../services/configuration.service';
import { EndpointFactory } from '../../services/endpoint-factory.service';
import { AuthService } from '../../services/auth.service';
import { catchError } from 'rxjs/operators';
import { Svc } from '../../models/svc.model';


@Injectable()
export class HomeService extends EndpointFactory{

  private readonly _svcsUsrl: string = '/api/Svc/';

  

  get svcsUrl() { return this.configurations.baseUrl + this._svcsUsrl; }



  constructor(authService:AuthService,http: HttpClient, configurations: ConfigurationService, injector: Injector) {

    super(http, configurations, injector);
  }

  public getDataTest() {
    return this.http.get<Svc[]>(this.svcsUrl,this.getRequestHeaders());
  }

  public getSvc<T>(from?: Date, to?: Date): Observable<T> {
    if (from == null || to == null) {
      return this.http.get<T>(this.svcsUrl,this.getRequestHeaders());
    }
    else {
      var url = `${this.svcsUrl}?from=${from.toLocaleDateString()}&to=${to.toLocaleDateString()}`;
      return this.http.get<T>(url, this.getRequestHeaders());
    }
  }

  public getDeleteSvc<T>(id: number):Observable<T> {
    const svcUrlRange = `${this.svcsUrl}/delete/${id}`;
    return this.http.get<T>(svcUrlRange, this.getRequestHeaders());
  }

  public getUpdateSvc<T>(svc: Svc):Observable<T> {
    const svcUrl =  `${this.svcsUrl}/update/${svc.id}`;
    return this.http.put<T>(svcUrl, svc, this.getRequestHeaders());
  }

  public getCreateSvc<T>(svc: Svc): Observable<T> {
    const svcUrl = `${this.svcsUrl}/create`;
    return this.http.put<T>(svcUrl, svc, this.getRequestHeaders());
  }


  public getIsLockedSvc<T>(id: number): Observable<T> {
    const svcUrl = `${this.svcsUrl}/locked/${id}`;
    return this.http.get<T>(svcUrl, this.getRequestHeaders());
  }



  public getLockSvc<T>(id: number): Observable<T> {
    const svcUrl = `${this.svcsUrl}/lock/${id}`;
    return this.http.get<T>(svcUrl, this.getRequestHeaders());
  }





}
