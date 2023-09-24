import 'dart:io';

import 'package:flutter/material.dart';
import 'package:mock_tests/locator.dart';
import 'package:mock_tests/ui/views/Home/home_page.dart';
import 'package:mock_tests/ui/views/Identity/login_page.dart';
import 'package:mock_tests/ui/views/Identity/register_page.dart';

void main() {
  // IMPORTANT: This is for testing purposes only. Do not use in production.
  HttpOverrides.global = MTMAHttpOverrides();
  setupLocator();
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Mock Tests',
      home: const HomePage(),
      routes: {
        // Identity pages
        LoginPage.id: (context) => const LoginPage(),
        RegisterPage.id: (context) => const RegisterPage(),
      },
    );
  }
}

// IMPORTANT: This overrides the certificate check. Do not use in production.
class MTMAHttpOverrides extends HttpOverrides {
  @override
  HttpClient createHttpClient(SecurityContext? context) {
    return super.createHttpClient(context)..badCertificateCallback = (X509Certificate cert, String host, int port) => true;
  }
}
