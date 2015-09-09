// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.
#I @"C:\Git\Weow\Temp\UnityVS_bin\Debug"
#r "Assembly-Csharp.dll"
#r "UnityEngine.dll"

#load "Library1.fs"
open FsharpTester

open Assets
open UnityEngine

let targets = [ for x in 1 .. 5 do
                yield new SimpleTargetable(new Vector3((float32)1,(float32)1,(float32)0) * (float32)x)]

let targeter = new LambdaTargeter((fun () -> Seq.cast targets),(fun()-> Vector3.zero))

targeter.Target targets.Head

targeter.CycleNextTarget()

targeter.CurrentTarget