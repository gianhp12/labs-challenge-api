import 'package:flutter_test/flutter_test.dart';
import 'package:labs_challenge_front/src/modules/auth/data/repositories/auth_repository_impl.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/actions/auth_register_actions.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/states/auth_register_state.dart';
import 'package:labs_challenge_front/src/shared/errors/common_errors.dart';
import 'package:mocktail/mocktail.dart';
import 'package:result_dart/result_dart.dart';

class MockAuthRepository extends Mock implements AuthRepositoryImpl {}

void main() {
  late MockAuthRepository mockRepository;
  late AuthRegisterActions actions;

  setUp(() {
    mockRepository = MockAuthRepository();
    actions = AuthRegisterActions(mockRepository);
  });

  group("register", () {
    test(
      "Should return success state when repository return success",
      () async {
        //GIVEN
        final username = "user-test";
        final email = "user@gmail.com";
        final password = "password-test";
        when(
          () => mockRepository.register(any(), any(), any()),
        ).thenAnswer((_) async => Success(1));
        //WHEN
        await actions.register(username, email, password);
        //THEN
        expect(actions.currentState, isA<SuccessAuthRegisterState>());
      },
    );

    test("Should return error state when repository return error", () async {
      //GIVEN
      final username = "user-test";
      final email = "user@gmail.com";
      final password = "password-test";
      when(() => mockRepository.register(any(), any(), any())).thenAnswer(
        (_) async => Failure(
          ConnectionError(
            errorMessage: 'error',
            errorModule: 'auth',
            errorMethod: 'login',
          ),
        ),
      );
      //WHEN
      await actions.register(username, email, password);
      //THEN
      expect(actions.currentState, isA<ErrorAuthRegisterState>());
    });
  });
}
