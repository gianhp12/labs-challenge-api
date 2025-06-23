import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/actions/auth_validate_token_actions.dart';
import 'package:labs_challenge_front/src/modules/auth/interactor/states/auth_validate_token_state.dart';
import 'package:labs_challenge_front/src/modules/auth/ui/widgets/auth_container.dart';
import 'package:labs_challenge_front/src/shared/hooks/use_state.dart';
import 'package:labs_challenge_front/src/shared/models/logged_user.dart';
import 'package:labs_challenge_front/src/shared/utils/form_validators.dart';
import 'package:labs_challenge_front/src/shared/widgets/app_button.dart';
import 'package:labs_challenge_front/src/shared/widgets/app_text_form_field.dart';

class ValidateTokenPage extends StatefulWidget {
  final LoggedUser loggedUser;
  const ValidateTokenPage({super.key, required this.loggedUser});

  @override
  State<ValidateTokenPage> createState() => _ValidateTokenPageState();
}

class _ValidateTokenPageState extends State<ValidateTokenPage> with UseState {
  final _actions = Modular.get<AuthValidateTokenActions>();
  final _formKey = GlobalKey<FormState>();
  final _tokenController = TextEditingController();
  final _tokenFocus = FocusNode();

  bool isLoading = false;
  String? errorMessage;

  @override
  void initState() {
   _actions.addListener(() {
      final state = _actions.currentState;
      if (state is SuccessAuthValidateTokenState) {
          Modular.to.navigate('/home');
      }
      setState(() {});
    });
    super.initState();
  }

  @override
  void dispose() {
    _tokenController.dispose();
    _tokenFocus.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final state = _actions.currentState;
    final isLoading = state is LoadingAuthValidateTokenState;
    final isSuccess = state is SuccessAuthValidateTokenState;
    final errorMessage =
        (state is ErrorAuthValidateTokenState) ? state.exception : null;
    final successResendTokenMessage =
        (state is SuccessAuthValidateResendTokenState)
            ? "Token solicitado com sucesso"
            : null;
    return Scaffold(
      backgroundColor: Colors.blue.shade50,
      body: Center(
        child: Form(
          key: _formKey,
          child: AuthContainer(
            subtitle:
                "Informe o token recebido no email para validar seu cadastro.",
            showBackButton: true,
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                AppTextFormField(
                  controller: _tokenController,
                  label: "Token",
                  icon: Icons.key,
                  focusNode: _tokenFocus,
                  autovalidateMode: AutovalidateMode.onUserInteraction,
                  validator:
                      (value) =>
                          FormValidators.isRequired(value, fieldName: 'Token'),
                ),
                const SizedBox(height: 10),
                TextButton(
                  onPressed:
                      isLoading
                          ? null
                          : () async {
                            await _actions.resendToken(widget.loggedUser.email);
                          },
                  child: Text(
                    'Reenviar token',
                    style: TextStyle(color: Colors.blue),
                  ),
                ),
                const SizedBox(height: 20),
                SizedBox(
                  width: double.infinity,
                  child: AppButton(
                    label: 'Validar',
                    isLoading: isLoading,
                    isSuccess: isSuccess,
                    backgroundColor: Colors.blue.shade700,
                    onPressed: () async {
                      if (_formKey.currentState!.validate()) {
                        await _actions.validateToken(
                          widget.loggedUser.email,
                          _tokenController.text,
                        );
                      }
                      await Future.delayed(const Duration(seconds: 2));
                    },
                  ),
                ),
                if (errorMessage != null) ...[
                  const SizedBox(height: 10),
                  Text(
                    errorMessage,
                    style: const TextStyle(color: Colors.red, fontSize: 14),
                  ),
                ],
                if (successResendTokenMessage != null) ...[
                  const SizedBox(height: 10),
                  Text(
                    successResendTokenMessage,
                    style: const TextStyle(color: Colors.green, fontSize: 14),
                  ),
                ],
              ],
            ),
          ),
        ),
      ),
    );
  }
}
