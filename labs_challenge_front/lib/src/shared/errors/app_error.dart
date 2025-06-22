abstract class AppError implements Exception {
  final bool saveInLog;
  final String? errorModule;
  final String? errorMethod;
  final String errorMessage;
  final String errorType;

  AppError({
    required this.saveInLog,
    required this.errorMessage,
    required this.errorType,
    this.errorModule,
    this.errorMethod,
  });
}
