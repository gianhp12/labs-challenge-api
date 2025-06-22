abstract class AppError implements Exception {
  final bool saveInLog;
  final String? errorModule;
  final String? errorMethod;
  final String errorMessage;

  AppError({
    required this.saveInLog,
    required this.errorMessage,
    this.errorModule,
    this.errorMethod,
  });
}
