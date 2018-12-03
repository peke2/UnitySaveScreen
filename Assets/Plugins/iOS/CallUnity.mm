#import <Foundation/Foundation.h>

extern "C" void CallUnity(const char *cbGameObjectName, const char *cbMethodName)
{
    NSLog(@"---- Unity側の呼び出し開始");
    UnitySendMessage(cbGameObjectName, cbMethodName, "Call from native");
    NSLog(@"---- Unity側の呼び出し終了");
}
