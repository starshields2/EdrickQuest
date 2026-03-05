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
//VAR failure = false
VAR passive = false
VAR type = 2
// ^^^ 0 = None, 1 = Mediation, 2 = Event, 3 = Cutscene

#Yael
The path forward has been destroyed. Look. The bridge is broken.
+[Continue. #EdrickContinue]
~IncreaseTension(1)
#Jasper
We can chop a tree down over the ravine and walk across.
++[Continue. #EdrickContinue]
#YaelAngry
That's too risky! I'll use one of my scrolls. We can easily use magic to levitate over the gap.
+++[Contine #EdrickContinue]
#Jasper
And waste precious scrolls? Don't be stupid. I'm knocking a tree down. 
++++[Let me sort this out. #EdrickContinue]  ->MEDIATIONSTART

=MEDIATIONSTART
#Edrick
What should we do?
+[We should use magic. #EdrickChoice]
~SetYaelsWay()
->YAEL
+[We should use the tree. #EdrickChoice]
~SetJaspersWay()
->JASPER
+[You two decide. #EdrickChoice] ->EDRICK
->DONE

=YAEL
#Yael
{yaelsWay: 
{tension > 13: You've made no mistake in listening to my wisdom!}
{tension <= 12: Let's see if this will work. }
}
+[Let's do this. #EdrickContinue]
~CalculateEventResults()
->YAELCOMP
->DONE

=JASPER
{jaspersWay: 
{tension > 13: Good thing you went with my idea, Calibrator! }
{tension <= 12: I hope it'll work, thanks for believing in me! }
}

+[Okay, let's try this. #EdrickContinue]
~CalculateEventResults()
->JASPERCOMP
->DONE

->END
=EDRICK
->DONE

=JASPERCOMP
{passive: You did nothing.}
{ success:
Jasper succeeded.
- else:
Jasper Failed.
}
->DONE
=YAELCOMP

{passive: You did nothing.}


{ success:
Yael succeeded.
- else:
Yael Failed.
}
->DONE


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
EXTERNAL CalculateEventResults()