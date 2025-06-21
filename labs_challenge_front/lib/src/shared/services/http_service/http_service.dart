import 'dart:convert';

import 'package:http/http.dart';
import 'package:labs_challenge_front/src/shared/services/http_service/http_error.dart';
import 'package:labs_challenge_front/src/shared/services/http_service/http_respose_model.dart';
import 'package:labs_challenge_front/src/shared/states/session_notifier.dart';

sealed class HttpService {
  Future<HttpResponse> post(
    String url, {
    Object? body,
    Map<String, String>? headers,
  });

  Future<HttpResponse> get(String url, {Map<String, String>? headers});

  Future<HttpResponse> put(
    String url, {
    Object? body,
    Map<String, String>? headers,
  });
}

class HttpServiceImpl implements HttpService {
  final Client _http;
  final SessionNotifier _sessionNotifier;

  HttpServiceImpl(this._http, this._sessionNotifier);

  @override
  Future<HttpResponse> get(String url, {Map<String, String>? headers}) async {
    var header = {'Content-Type': 'application/json'};
    if (headers != null) header.addAll(headers);
    final String request = 'GET - url: $url, header: $header, body:""';
    try {
      final response = await _http.get(Uri.parse(url), headers: header);
      final hasError = _verifyHttpError(
        response.statusCode,
        response.body,
        request,
      );
      if (hasError != null) {
        throw hasError;
      }
      return HttpResponse(
        data: response.body,
        statusCode: response.statusCode,
        statusMsg: response.reasonPhrase,
        requestBody: request,
      );
    } catch (e) {
      if (e is RemoteRequestError) rethrow;
      throw RemoteRequestError(error: e.toString(), bodyRequest: request);
    }
  }

  @override
  Future<HttpResponse> post(
    String url, {
    Object? body,
    Map<String, String>? headers,
  }) async {
    var header = {'Content-Type': 'application/json'};
    if (headers != null) header.addAll(headers);
    final String bodyS = jsonEncode(body);
    final String request = 'POST - url: $url, header: $header, body: $bodyS';
    try {
      final response = await _http.post(
        Uri.parse(url),
        body: bodyS,
        headers: header,
      );
      final hasError = _verifyHttpError(
        response.statusCode,
        response.body,
        request,
      );
      if (hasError != null) {
        throw hasError;
      }
      return HttpResponse(
        data: response.body,
        statusCode: response.statusCode,
        statusMsg: response.reasonPhrase,
        requestBody: request,
      );
    } catch (e) {
      if (e is RemoteRequestError) rethrow;
      throw RemoteRequestError(error: e.toString(), bodyRequest: request);
    }
  }

  @override
  Future<HttpResponse> put(
    String url, {
    Object? body,
    Map<String, String>? headers,
  }) async {
    var header = {'Content-Type': 'application/json'};
    if (headers != null) header.addAll(headers);
    final String bodyS = jsonEncode(body);
    final String request = 'PUT - url: $url, header: $header, body: $bodyS';
    try {
      final response = await _http.put(
        Uri.parse(url),
        body: bodyS,
        headers: header,
      );
      final hasError = _verifyHttpError(
        response.statusCode,
        response.body,
        request,
      );
      if (hasError != null) {
        throw hasError;
      }
      return HttpResponse(
        data: response.body,
        statusCode: response.statusCode,
        statusMsg: response.reasonPhrase,
        requestBody: request,
      );
    } catch (e) {
      if (e is RemoteRequestError) rethrow;
      throw RemoteRequestError(error: e.toString(), bodyRequest: request);
    }
  }

  RemoteRequestError? _verifyHttpError(
    int statusCode,
    String? body,
    String request,
  ) {
    switch (statusCode) {
      case 422:
        return UnprocessableEntity(error: body, bodyRequest: request);
      case 404:
        return NotFound(error: body, bodyRequest: request);
      case 500:
        return InternalServerError(error: body, bodyRequest: request);
      case 403:
        return Forbidden(error: body, bodyRequest: request);
      case 401:
        _sessionNotifier.setSessionExpired(true);
        return Unauthorized(error: body, bodyRequest: request);
      case 503:
        return ServiceUnavaliable(error: body, bodyRequest: request);
      case 400:
        return BadRequest(error: body, bodyRequest: request);
      case 408:
        return RequestTimeOut(error: body, bodyRequest: request);
      case 504:
        return GatewayTimeOut(error: body, bodyRequest: request);
    }

    if (statusCode > 299) {
      return RemoteRequestError(
        error: statusCode.toString(),
        bodyRequest: request,
      );
    }

    return null;
  }
}
