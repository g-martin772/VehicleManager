# Vehicle Manager Software

This is meant as an educational software showcase.

## Technology

- .NET 8 (C#)
- Blazor Server
- XUnit
- Newtonsoft.Json

## Original Asignment

Aufgabe - Teamwork Auto
Due 9 October 2024 23:59
Instructions
Entwickeln Sie ein Fahrzeugmanagement-System, das verschiedene Fahrzeugtypen verwaltet und steuert. Beginnen Sie mit der Definition einer abstrakten Klasse für Fahrzeuge, die Modell und Geschwindigkeit als Eigenschaften enthält und Methoden zum Beschleunigen und Bremsen bereitstellt. Erstellen Sie konkrete Klassen für verschiedene Fahrzeugtypen wie Autos, Lastwagen und Motorräder, die diese abstrakte Klasse erweitern und spezifische Implementierungen der Beschleunigungs- und Bremsmethoden bieten.

Fügen Sie eine Möglichkeit hinzu, verschiedene Operationen auf den Fahrzeugen auszuführen. Definieren Sie eine Schnittstelle für Operationen, die unterschiedliche Typen von Befehlen wie Beschleunigen und Bremsen auf Fahrzeuge anwenden können. Erstellen Sie konkrete Implementierungen dieser Operationen, die jeweils auf ein Fahrzeug angewendet werden. Implementieren Sie eine Klasse, die diese Befehle speichert und nacheinander ausführt.

Ergänzen Sie das System um unterschiedliche Fahrverhalten, die zur Laufzeit geändert werden können. Definieren Sie eine Schnittstelle für verschiedene Fahrstrategien und erstellen Sie Implementierungen für verschiedene Fahrverhalten wie normales, sportliches und energieeffizientes Fahren. Die Fahrzeuge sollen in der Lage sein, ihre Fahrstrategie zur Laufzeit zu ändern und sich entsprechend zu verhalten.

Implementieren Sie zusätzlich eine Verwaltung von Fahrzeugzuständen. Definieren Sie verschiedene Zustände, die ein Fahrzeug annehmen kann, wie geparkt, fahrend und in Wartung. Jeder Zustand soll spezifische Verhaltensweisen für das Fahrzeug bieten, z.B. wie es sich beim Beschleunigen oder Bremsen verhält. Der Fahrzeugzustand soll zur Laufzeit geändert werden können, und das Fahrzeug soll entsprechend des aktuellen Zustands agieren.

Erstellen Sie Testfälle, um sicherzustellen, dass alle Teile des Systems korrekt funktionieren. Überprüfen Sie die Iteration über die Fahrzeugkollektion, die Ausführung von Operationen, die Änderung und Anwendung von Fahrstrategien sowie die Verwaltung und Änderung von Fahrzeugzuständen. Stellen Sie sicher, dass die verschiedenen Verhaltensweisen und Zustände korrekt umgesetzt und in der Anwendung reflektiert werden.

Phase 1:
Reflektiere diese Angabe, welche Design Pattern würdest du beim Implementierung benutzen? 
Wenn ihr die Design Pattern herausgefunden habt, schreibt eine Teamsnachricht und ihr erhaltet eine Antwort ob die Pattern passen.
Hinweis: Es könnte ein neues Pattern enthalten sein.

Phase 2:
Zeichne eine Klassendiagramm für die Implementierung der Angabe und baue die oben definierten Design Pattern ein.
Gebt das Klassendiagramm ab. 

Phase 3: 
Implementiere die Angabe in einem gemeinsamen Projekt, Verwaltung über GIT. Nutze eine DLL für die Fahrzeugverwaltung, überlege wie eine Sinnvolle Lösung aussehen könnte für alle Rückgabewerte (Zeichenketten) die während dem Ablauf erzeugt werden. 
