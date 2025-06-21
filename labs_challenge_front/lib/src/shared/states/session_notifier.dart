import 'package:flutter/material.dart';
import 'package:labs_challenge_front/src/shared/models/logged_user.dart';
import 'package:labs_challenge_front/src/shared/states/session_state.dart';

class SessionNotifier extends ChangeNotifier {
  SessionState _state = SessionState();

  SessionState get state => _state;

  void logIn(LoggedUser user) {
    _state = _state.copyWith(
      loggedUser: user,
      sessionExpired: false,
    );
    notifyListeners();
  }

  void logOut() {
    _state = _state.copyWith(
      loggedUser: null,
      sessionExpired: false,
      viewsVisited: [],
    );
    notifyListeners();
  }

  void setSessionExpired(bool value) {
    _state = _state.copyWith(sessionExpired: value);
    notifyListeners();
  }

  void saveAccessedView(String view) {
    final updatedViews = List<String>.from(_state.viewsVisited)..add(view);
    _state = _state.copyWith(viewsVisited: updatedViews);
    notifyListeners();
  }
}
