//InkOnly
VAR YaelTell = 0
VAR jaspersWay = false
VAR yaelsWay = false
VAR YaelExhaust = 0
VAR JasperExhaust = 0

//External Variables
VAR ExternalTutorialNum = 0
VAR PopupNum = 0
VAR tension = 14
VAR NotesIndex = 0
VAR success = false
VAR failure = true
VAR passive = false
VAR type = 2
// ^^^ 0 = None, 1 = Mediation, 2 = Event, 3 = Cutscene

#Yael
Look, in the bushes. A shrine. I should send a prayer to Elunia immediately.
+[Continue. #EdrickContinue]
~IncreaseTension(1)
#Jasper
A solar whetstone! Once I remove it from this rock, my tools will be sharp for weeks.
++[Continue. #EdrickContinue]
#Yael
You can't possibly want to use this for sharpening tools. This is a sacred place.
+++[Contine #EdrickContinue]
#Jasper
You see the gleaning whetstone, yes? Dead gods can't help us, steel can. 
++++[I can also help you. #EdrickContinue]  ->MEDIATIONSTART

=MEDIATIONSTART
#Edrick
What should we do?
+[We should keep the stone. #EdrickChoice]
~SetYaelsWay()
->YAEL
+[We should use the whetstone. #EdrickChoice]
~SetJaspersWay()
->YAEL
+[You two decide. #EdrickChoice] ->COMP
->DONE

=YAEL
#Yael
{yaelsWay: 
{tension > 13: You've made no mistake in listening to my wisdom!}
{tension <= 12: Let's see if this will work. }
}

{jaspersWay: 
{tension > 13: Good thing you went with my idea, Calibrator! }
{tension <= 12: I hope it'll work, thanks for believing in me! }
}

+[Okay. #EdrickChoice] ->COMP
->DONE

->END

=COMP
{success:  You passed.}
{passive: You did nothing.}
{failure: You failed}

->DONE

==function YaelTellFalse(amount)
~YaelTell = 1
~return YaelTell

==function YaelTellTrue(amount)
~YaelTell = 2

==function IncreaseTension(amount)
~tension = tension + amount

==function DecreaseTension(amount)
~tension = tension - amount

==function SetJaspersWay
~jaspersWay = true

==function SetYaelsWay
~yaelsWay = true

==function ChangeNotesIndex(amount)
~NotesIndex = amount

==function ExtendedTutorial(amount)
~ExternalTutorialNum = amount

==function UpdatePopupNumber(amount)
~PopupNum = amount

==function IncreaseYaelExhaust(amount)
~YaelExhaust = YaelExhaust + amount

EXTERNAL UpdateNote()
EXTERNAL ShowObjection()