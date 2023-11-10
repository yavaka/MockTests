// ignore_for_file: unnecessary_this, use_build_context_synchronously

import 'package:flutter/material.dart';
import 'package:local_session_timeout/local_session_timeout.dart';
import 'package:mock_tests/core/configuration/locator.dart';
import 'package:mock_tests/core/service_models/identity/login_service_model.dart';
import 'package:mock_tests/core/services/common/local_session/local_session_service_abstract.dart';
import 'package:mock_tests/ui/components/identity_button.dart';
import 'package:mock_tests/ui/controllers/identity_controller.dart';
import 'package:mock_tests/ui/views/account/dashboard_page.dart';
import 'package:shared_preferences/shared_preferences.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({super.key});

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  final _identityController = serviceLocator<IdentityController>();
  final _localSessionService = serviceLocator<LocalSessionServiceAbstract>();
  final GlobalKey<FormState> _loginFormKey = GlobalKey<FormState>();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  String? loginError;
  late SharedPreferences _prefs;

  @override
  void initState() {
    super.initState();
    initSharedPrefs();
  }

  void initSharedPrefs() async {
    this._prefs = await SharedPreferences.getInstance();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        // Form
        child: Form(
          key: _loginFormKey,
          child: SingleChildScrollView(
            child: Padding(
              padding: const EdgeInsets.all(30.0),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  const SizedBox(height: 100),
                  const Center(
                    child: Text(
                      "Welcome back",
                      style: TextStyle(
                        fontSize: 20,
                        fontWeight: FontWeight.w600,
                      ),
                    ),
                  ),

                  // Username Input -------------------------------------
                  const SizedBox(height: 100),
                  TextFormField(
                    controller: _emailController,
                    decoration: const InputDecoration(
                      hintText: "email",
                    ),
                  ),

                  // Password Input -------------------------------------
                  const SizedBox(height: 20),
                  TextFormField(
                    controller: _passwordController,
                    obscureText: true,
                    decoration: const InputDecoration(
                      hintText: "password",
                    ),
                  ),

                  // Login Button ---------------------------------------
                  const SizedBox(height: 120),
                  IdentityButton(title: 'Login', onPressed: loginButtonPressed),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }

  void loginButtonPressed() async {
    if (_loginFormKey.currentState!.validate()) {
      var result = await _identityController.login(
        LoginServiceModel(
          email: _emailController.text,
          password: _passwordController.text,
        ),
      );

      if (result.succeeded) {
        this._prefs.setString('token', result.token!);
        this._localSessionService.sessionStateStream.add(SessionState.startListening);

        Navigator.of(context).push(
          MaterialPageRoute(
            builder: (context) => const DashboardPage(),
          ),
        );
      } else {
        setState(() {
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(
              content: Text(result.error!),
            ),
          );
        });
      }
    }
  }

  @override
  void dispose() {
    _emailController.dispose();
    _passwordController.dispose();
    super.dispose();
  }
}
