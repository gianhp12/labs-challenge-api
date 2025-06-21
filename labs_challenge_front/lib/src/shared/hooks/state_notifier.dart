import 'package:flutter/material.dart';

class AppState {}

class StateNotifier<T extends AppState> extends ChangeNotifier {
  T _state;

  T get currentState => _state;

  StateNotifier(T initialState) : _state = initialState;

  void notifySetState(T Function(T state) changeState) {
    _state = changeState(_state);
    notifyListeners();
  }

  void setState(T Function(T state) changeState) {
    _state = changeState(_state);
  }
}
