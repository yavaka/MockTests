import 'dart:async';

import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';
import 'package:local_session_timeout/local_session_timeout.dart';
import 'package:mock_tests/core/services/common/local_session/local_session_service_abstract.dart';
import 'package:mock_tests/ui/views/identity/login_page.dart';
import 'package:shared_preferences/shared_preferences.dart';

class LocalSessionService extends LocalSessionServiceAbstract implements Disposable {
  final _sessionConfig = SessionConfig(
    invalidateSessionForAppLostFocus: const Duration(minutes: 2), // 2 mins
    invalidateSessionForUserInactivity: const Duration(minutes: 3), // 3 mins
  );
  final StreamController<SessionState> _sessionStateStream = StreamController<SessionState>();

  @override
  StreamController<SessionState> get sessionStateStream => _sessionStateStream;

  @override
  SessionConfig get sessionConfig => _sessionConfig;

  @override
  void startListening({required GlobalKey<NavigatorState> navigatorKey}) {
    sessionConfig.stream.listen((SessionTimeoutState timeoutEvent) async {
      var pref = await SharedPreferences.getInstance();
      // It must always contain token because the local session start listening on user login
      if (pref.containsKey('token')) {
        pref.remove('token');
      }

      sessionStateStream.add(SessionState.stopListening);

      // handle user  inactive timeout
      navigatorKey.currentState!.popUntil((route) => route.isFirst);
      navigatorKey.currentState!.push(MaterialPageRoute(
        builder: (_) => const LoginPage(),
      ));
    });
  }

  @override
  FutureOr onDispose() {
    sessionConfig.stream.drain();
  }
}
