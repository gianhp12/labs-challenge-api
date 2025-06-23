import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';
import 'package:labs_challenge_front/src/shared/models/logged_user.dart';

sealed class HomeState extends AppState {
  final bool loading;
  final String exception;
  final LoggedUser? user;

  HomeState({required this.loading, required this.exception, this.user});

  factory HomeState.start() => StartHomeState();

  HomeState setLoading() => LoadingHomeState(loading: true);

  HomeState setError(String error) => ErrorHomeState(exception: error);

  HomeState setUser(LoggedUser user) => GettedHomeState(user: user);
}

class StartHomeState extends HomeState {
  StartHomeState({super.loading = false, super.exception = ''});
}

class LoadingHomeState extends HomeState {
  LoadingHomeState({required super.loading, super.exception = ''});
}

class ErrorHomeState extends HomeState {
  ErrorHomeState({required super.exception, super.loading = false});
}

class GettedHomeState extends HomeState {
  GettedHomeState({
    required super.user,
    super.loading = false,
    super.exception = '',
  });
}
