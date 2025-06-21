class HttpResponse {
  final dynamic data;
  final int statusCode;
  final String? statusMsg;
  final String requestBody;

  HttpResponse({
    required this.data,
    required this.statusCode,
    required this.statusMsg,
    required this.requestBody,
  });
}
