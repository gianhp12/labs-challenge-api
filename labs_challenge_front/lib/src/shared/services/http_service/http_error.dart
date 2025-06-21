class RemoteRequestError implements Exception {
  final String? error;
  final String? bodyRequest;

  RemoteRequestError({this.error, this.bodyRequest});
}

class RequestTimeOut extends RemoteRequestError {
  RequestTimeOut({super.error, super.bodyRequest});
}

class Unauthorized extends RemoteRequestError {
  Unauthorized({super.error, super.bodyRequest});
}

class NotFound extends RemoteRequestError {
  NotFound({super.error, super.bodyRequest});
}

class InternalServerError extends RemoteRequestError {
  InternalServerError({super.error, super.bodyRequest});
}

class AccessDenied extends RemoteRequestError {
  AccessDenied({super.error, super.bodyRequest});
}

class ServiceUnavaliable extends RemoteRequestError {
  ServiceUnavaliable({super.error, super.bodyRequest});
}

class BadRequest extends RemoteRequestError {
  BadRequest({super.error, super.bodyRequest});
}

class GatewayTimeOut extends RemoteRequestError {
  GatewayTimeOut({super.error, super.bodyRequest});
}

class Forbidden extends RemoteRequestError {
  Forbidden({super.error, super.bodyRequest});
}

class UnprocessableEntity extends RemoteRequestError {
  UnprocessableEntity({super.error, super.bodyRequest});
}
