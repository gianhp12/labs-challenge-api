import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';
import 'package:labs_challenge_front/src/shared/models/logged_user.dart';

class SessionState extends AppState {
  final LoggedUser? loggedUser;
  final bool sessionExpired;
  final List<String> viewsVisited;

  SessionState({
    this.loggedUser,
    this.sessionExpired = false,
    List<String>? viewsVisited,
  }) : viewsVisited = viewsVisited ?? [];

  SessionState copyWith({
    LoggedUser? loggedUser,
    bool? sessionExpired,
    List<String>? viewsVisited,
  }) {
    return SessionState(
      loggedUser: loggedUser ?? this.loggedUser,
      sessionExpired: sessionExpired ?? this.sessionExpired,
      viewsVisited: viewsVisited ?? this.viewsVisited,
    );
  }
}
