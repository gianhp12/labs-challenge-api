import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:labs_challenge_front/src/shared/models/logged_user.dart';
import 'package:labs_challenge_front/src/shared/states/session_state.dart';
import 'package:shared_preferences/shared_preferences.dart';

class SessionNotifier extends ChangeNotifier {
  SessionState _state = SessionState();
  String? sessionMessage;
  bool? isMessageSuccess;
  SessionState get state => _state;

  void logIn(LoggedUser user) {
    _state = _state.copyWith(loggedUser: user, sessionExpired: false);
    sessionMessage = null;
    notifyListeners();
  }

  void logOut(String? message, bool isSuccess) async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove('logged_user');
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
    final prefs = await SharedPreferences.getInstance();
    final userJson = prefs.getString('logged_user');
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
