import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:labs_challenge_front/src/shared/models/logged_user.dart';
import 'package:labs_challenge_front/src/shared/services/local_storage/shared_preferences_adapter.dart';
import 'package:labs_challenge_front/src/shared/states/session_state.dart';

class SessionNotifier extends ChangeNotifier {
  final LocalStorage localStorage;
  SessionState _state = SessionState();
  String? sessionMessage;
  bool? isMessageSuccess;
  SessionState get state => _state;

  SessionNotifier({required this.localStorage});

  void logIn(LoggedUser user) async {
    _state = _state.copyWith(loggedUser: user, sessionExpired: false);
    sessionMessage = null;
    await localStorage.saveString('logged_user', jsonEncode(user.toMap()));
    notifyListeners();
  }

  void logOut(String? message, bool isSuccess) async {
    await localStorage.remove('logged_user');
    _state = SessionState();
    sessionMessage = message;
    isMessageSuccess = isSuccess;
    notifyListeners();
  }

  void setSessionExpired(bool value) {
    _state = _state.copyWith(sessionExpired: value);
    notifyListeners();
  }

  Future<void> loadSession() async {
    final userJson = await localStorage.getString('logged_user');
    if (userJson != null) {
      final userMap = jsonDecode(userJson);
      final user = LoggedUser.fromMap(userMap);
      logIn(user);
    }
  }

  void saveAccessedView(String view) {
    final updatedViews = List<String>.from(_state.viewsVisited)..add(view);
    _state = _state.copyWith(viewsVisited: updatedViews);
    notifyListeners();
  }

  void setSessionMessage(String message) {
    sessionMessage = message;
    notifyListeners();
  }

  void clearSessionMessage() {
    sessionMessage = null;
    notifyListeners();
  }
}
