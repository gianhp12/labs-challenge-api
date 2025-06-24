import 'package:flutter/material.dart';
import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';

mixin UseState<T extends StatefulWidget> on State<T> {
  final _listeners = <StateNotifier>[];

  void _listener() {
    if (mounted) setState(() {});
  }

  void addStateListener(StateNotifier listenable) {
    if (!_listeners.contains(listenable)) {
      _listeners.add(listenable);
      listenable.addListener(_listener);
    }
  }

  @override
  void dispose() {
    for (var listenable in _listeners) {
      listenable.removeListener(_listener);
    }
    super.dispose();
  }
}
