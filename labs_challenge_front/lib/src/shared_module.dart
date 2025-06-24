import 'package:flutter_modular/flutter_modular.dart';
import 'package:http/http.dart';
import 'package:labs_challenge_front/src/shared/services/http_service/http_service.dart';
import 'package:labs_challenge_front/src/shared/services/local_storage/shared_preferences_adapter.dart';

class SharedModule extends Module {
  @override
  void exportedBinds(Injector i) {
    i.addInstance<Client>(Client());
    i.add<LocalStorage>(SharedPreferencesAdapter.new);
    i.add<HttpService>(HttpServiceImpl.new);
  }
}
