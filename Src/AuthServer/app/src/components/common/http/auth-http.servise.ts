import {Injectable} from "@angular/core";
import {IAuthHttp} from "./i-auth-http.servise";
import {Http, RequestOptionsArgs} from "@angular/http";
import {Observable} from "rxjs/Observable";


@Injectable()
export class AuthHttp implements IAuthHttp {
    request(url: string | Request, options?: RequestOptionsArgs): Observable<Response> {
        throw new Error('Method not implemented.');
    }

    get(url: string, options?: RequestOptionsArgs): Observable<Response> {
        throw new Error('Method not implemented.');
    }

    post(url: string, body: any, options?: RequestOptionsArgs): Observable<Response> {
        throw new Error('Method not implemented.');
    }

    put(url: string, body: any, options?: RequestOptionsArgs): Observable<Response> {
        throw new Error('Method not implemented.');
    }

    delete(url: string, options?: RequestOptionsArgs): Observable<Response> {
        throw new Error('Method not implemented.');
    }

    patch(url: string, body: any, options?: RequestOptionsArgs): Observable<Response> {
        throw new Error('Method not implemented.');
    }

    head(url: string, options?: RequestOptionsArgs): Observable<Response> {
        throw new Error('Method not implemented.');
    }

    options(url: string, options?: RequestOptionsArgs): Observable<Response> {
        throw new Error('Method not implemented.');
    }


    constructor(private http: Http) {}



}