import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/home/interactor/states/home_state.dart';
import 'package:labs_challenge_front/src/shared/hooks/state_notifier.dart';
import 'package:labs_challenge_front/src/shared/states/session_notifier.dart';

class HomeActions extends StateNotifier<HomeState> {
  HomeActions() : super(HomeState.start());

  void logout() async {
    final session = Modular.get<SessionNotifier>();
    session.logOut();
  }

  void getInfoUser() async {
    notifySetState((state) => state.setLoading());
    final session = Modular.get<SessionNotifier>();
    var user = session.state.loggedUser;
    notifySetState((state) => state.setUser(user!));
  }
}
