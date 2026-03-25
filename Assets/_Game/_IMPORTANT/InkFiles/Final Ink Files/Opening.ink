//InkOnly
VAR YaelTell = 0
VAR jasperVocab = false
VAR yaelRitual = false
VAR YaelExhaust = 0
VAR JasperExhaust = 0

//External Variables
VAR ExternalTutorialNum = 0
VAR PopupNum = 0
VAR tension = 6
VAR NotesIndex = 0
VAR success = false
VAR failure = true
VAR passive = false
VAR type = 2
// ^^^ 0 = None, 1 = Mediation, 2 = Event, 3 = Cutscene

#Edrick
There is not much time.
+[Continue #EdrickContinue]
#Yael
You can take us there, right? To the Temple of Scales?
++[I can take you there. #EdrickChoice]
#YaelPensive
You’re going to have to. The sooner we get answers, the better. Maybe they will be so astounding, Jasper will finally shut up.
++[Only if you two stop bickering. #EdrickChoice]
#Jasper
I have complete faith in you, dear Calibrator! Even if Yael yaps my ears off along the way!
+++[Continue #EdrickContinue]
#Edrick
We must consult the map.
->END