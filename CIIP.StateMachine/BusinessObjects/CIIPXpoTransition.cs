using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.StateMachine;
using DevExpress.ExpressApp.StateMachine.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using CIIP;

namespace CIIP.StateMachine
{
    [DefaultProperty("Caption"), XafDisplayName("ת��"), ImageName("BO_Transition")]
    public class CIIPXpoTransition : StateMachineObjectBase, ITransition, ITransitionUISettings,IFlowAction
    {
        public CIIPXpoTransition(Session session)
            : base(session)
        {

        }

        [Browsable(false)]
        public int BeginItemPointIndex { get; set; }

        [Browsable(false)]
        public int EndItemPointIndex { get; set; }

        [XafDisplayName("����")]
        public string Caption
        {
            get
            {
                string propertyValue = base.GetPropertyValue<string>("Caption");
                if (string.IsNullOrEmpty(propertyValue) && (this.TargetState != null))
                {
                    propertyValue = this.TargetState.Caption;
                }
                return propertyValue;
            }
            set
            {
                base.SetPropertyValue<string>("Caption", value);
            }
        }

        IState ITransition.TargetState
        {
            get
            {
                return this.TargetState;
            }
        }

        [XafDisplayName("����")]
        public int Index
        {
            get
            {
                return base.GetPropertyValue<int>("Index");
            }
            set
            {
                base.SetPropertyValue<int>("Index", value);
            }
        }
        [XafDisplayName("���沢�ر���ͼ")]
        public bool SaveAndCloseView
        {
            get
            {
                return base.GetPropertyValue<bool>("SaveAndCloseView");
            }
            set
            {
                base.SetPropertyValue<bool>("SaveAndCloseView", value);
            }
        }

        [Association("State-Transitions"), RuleRequiredField("GepXpoTransition.SourceState", DefaultContexts.Save)]
        [XafDisplayName("��Դ״̬")]
        public CIIPXpoState SourceState
        {
            get
            {
                return base.GetPropertyValue<CIIPXpoState>("SourceState");
            }
            set
            {
                base.SetPropertyValue("SourceState", value);
            }
        }

        [ImmediatePostData, DataSourceProperty("SourceState.StateMachine.States"), RuleRequiredField("GepXpoTransition.TargetState", DefaultContexts.Save)]
        [XafDisplayName("Ŀ��״̬")]
        public CIIPXpoState TargetState
        {
            get
            {
                return base.GetPropertyValue<CIIPXpoState>("TargetState");
            }
            set
            {
                base.SetPropertyValue("TargetState", value);
                base.OnChanged("Caption");
            }
        }

        IFlowNode IFlowAction.From
        {
            get
            {
                return SourceState;
            }

            set
            {
                SourceState = (CIIPXpoState)value;
            }
        }

        IFlowNode IFlowAction.To
        {
            get
            {
                return TargetState;
            }

            set
            {
                TargetState = (CIIPXpoState)value;
            }
        }

        
    }
}