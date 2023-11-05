// ignore_for_file: must_be_immutable

import 'dart:io';

import 'package:flutter/material.dart';
import 'package:local_session_timeout/local_session_timeout.dart';
import 'package:mock_tests/core/configuration/locator.dart';
import 'package:mock_tests/core/services/common/local_session/local_session_service_abstract.dart';
import 'package:mock_tests/ui/views/home/home_page.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();

  // IMPORTANT: This is for testing purposes only. Do not use in production.
  HttpOverrides.global = MTMAHttpOverrides();
  setupLocator();
  await serviceLocator.allReady();
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  final LocalSessionServiceAbstract _localSessionService = serviceLocator<LocalSessionServiceAbstract>();
  final _navigatorKey = GlobalKey<NavigatorState>();

  MyApp({super.key}) {
    _localSessionService.startListening(navigatorKey: _navigatorKey);
  }

  @override
  Widget build(BuildContext context) {
    return SessionTimeoutManager(
      userActivityDebounceDuration: const Duration(seconds: 1),
      sessionConfig: _localSessionService.sessionConfig,
      sessionStateStream: _localSessionService.sessionStateStream.stream,
      child: MaterialApp(
        navigatorKey: _navigatorKey,
        debugShowCheckedModeBanner: false,
        title: 'Mock Tests',
        home: const HomePage(),
      ),
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
