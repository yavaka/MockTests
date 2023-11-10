// ignore_for_file: avoid_unnecessary_containers, prefer_const_constructors, prefer_const_literals_to_create_immutables

import 'package:flutter/material.dart';
import 'package:mock_tests/core/common/constants.dart';
import 'package:mock_tests/ui/components/identity_button.dart';
import 'package:mock_tests/ui/views/Identity/login_page.dart';
import 'package:mock_tests/ui/views/account/dashboard_page.dart';
import 'package:shared_preferences/shared_preferences.dart';

class HomePage extends StatefulWidget {
  const HomePage({super.key});

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  late bool _isLoggedIn = false;

  void setIsUserLoggedIn() async {
    var pref = await SharedPreferences.getInstance();
    _isLoggedIn = pref.containsKey('token');
  }

  @override
  void initState() {
    super.initState();
    setIsUserLoggedIn();
  }

  @override
  Widget build(BuildContext context) {
    if (_isLoggedIn) {
      return DashboardPage();
    }

    return Container(
      color: Colors.white,
      child: Stack(
        children: <Widget>[
          Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              // Title
              Text(
                "Mock Tests",
                textAlign: TextAlign.center,
                style: TextStyle(
                  fontSize: 30,
                  color: kPrimaryLightColor,
                  decoration: TextDecoration.none,
                ),
              ),
              SizedBox(
                height: 150,
              ),

              // Login Button
              IdentityButton(
                title: 'Login',
                onPressed: () => Navigator.of(context).push(
                  MaterialPageRoute(
                    builder: (context) => LoginPage(),
                  ),
                ),
              ),
              SizedBox(
                height: 20,
              ),

              // Register Button
              IdentityButton(
                title: 'Register',
                onPressed: () => Navigator.of(context).push(
                  MaterialPageRoute(
                    builder: (context) => LoginPage(),
                  ),
                ),
              ),
            ],
          )
        ],
      ),
    );
  }
}
