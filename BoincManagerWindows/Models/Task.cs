﻿using System.Collections.Generic;
using System.Linq;
using BoincManager;
using BoincManager.Interfaces;
using BoincManager.Models;

namespace BoincManagerWindows.Models
{
    class Task : BindableBase, ITask, IFilterable
    {
        public int HostId { get; }
        public string HostName { get; }

        string project;
        public string Project
        {
            get { return project; }
            private set { SetProperty(ref project, value); }
        }

        string application;
        public string Application
        {
            get { return application; }
            private set { SetProperty(ref application, value); }
        }

        string workunit;
        public string Workunit
        {
            get { return workunit; }
            private set { SetProperty(ref workunit, value); }
        }

        double progress;
        public double Progress
        {
            get { return progress; }
            private set { SetProperty(ref progress, value); }
        }

        string elapsedTime;
        public string ElapsedTime
        {
            get { return elapsedTime; }
            private set { SetProperty(ref elapsedTime, value); }
        }

        string cpuTime;
        public string CpuTime
        {
            get { return cpuTime; }
            private set { SetProperty(ref cpuTime, value); }
        }

        string cpuTimeRemaining;
        public string CpuTimeRemaining
        {
            get { return cpuTimeRemaining; }
            private set { SetProperty(ref cpuTimeRemaining, value); }
        }

        string lastCheckpoint;
        public string LastCheckpoint
        {
            get { return lastCheckpoint; }
            private set { SetProperty(ref lastCheckpoint, value); }
        }

        string deadline;
        public string Deadline
        {
            get { return deadline; }
            private set { SetProperty(ref deadline, value); }
        }

        string status;
        public string Status
        {
            get { return status; }
            private set { SetProperty(ref status, value); }
        }

        public BoincRpc.Result RpcResult { get; private set; }
        public BoincRpc.Project RpcProject { get; private set; }
        public BoincRpc.Workunit RpcWorkunit { get; private set; }
        public BoincRpc.App RpcApp { get; private set; }
        public BoincRpc.AppVersion RpcAppVersion { get; private set; }
        
        public Task(HostState hostState)
        {
            HostId = hostState.Id;
            HostName = hostState.Name;
        }

        public void Update(BoincRpc.Result result, HostState hostState)
        {
            RpcProject = hostState.BoincState.Projects?.FirstOrDefault(p => p.MasterUrl == result.ProjectUrl);
            RpcWorkunit = hostState.BoincState.Workunits?.FirstOrDefault(w => w.ProjectUrl == RpcProject?.MasterUrl && w.Name == result.WorkunitName);
            RpcApp = hostState.BoincState.Apps?.FirstOrDefault(a => a.ProjectUrl == RpcProject?.MasterUrl && a.Name == RpcWorkunit?.AppName);

            //RpcAppVersion = result.VersionNumber != 0
            //    ? boincState.AppVersions?.FirstOrDefault(av => av.ProjectUrl == result.ProjectUrl && av.AppName == RpcApp?.Name && av.VersionNumber == result.VersionNumber && av.PlanClass == result.PlanClass)
            //    : boincState.AppVersions?.FirstOrDefault(av => av.ProjectUrl == result.ProjectUrl && av.AppName == RpcApp?.Name && av.VersionNumber == RpcWorkunit?.VersionNumber);

            if (RpcProject == null || RpcWorkunit == null || RpcApp == null/* || RpcAppVersion == null*/)
            {
                Status = Statuses.GetTaskStatus(result, RpcProject, hostState.BoincState);
                return;
            }

            RpcResult = result;

            Project = RpcProject.ProjectName;
            Application = RpcApp.UserFriendlyName;
            Workunit = result.WorkunitName;
            Progress = result.ReadyToReport ? 1.0 : result.FractionDone;
            ElapsedTime = BoincManager.Utils.ConvertDuration(result.ElapsedTime);
            CpuTime = BoincManager.Utils.ConvertDuration(result.CurrentCpuTime);
            CpuTimeRemaining = BoincManager.Utils.ConvertDuration(result.EstimatedCpuTimeRemaining);
            LastCheckpoint = BoincManager.Utils.ConvertDuration(result.CurrentCpuTime - result.CheckpointCpuTime);
            Deadline = BoincManager.Utils.ConvertDateTime(result.ReportDeadline);
            Status = Statuses.GetTaskStatus(result, RpcProject, hostState.BoincState);
        }
        
        public IEnumerable<string> GetContentsForFiltering()
        {
            yield return HostName;
            yield return Project;
            yield return Application;
            yield return Workunit;
            yield return CpuTime;
            yield return CpuTimeRemaining;
            yield return LastCheckpoint;
            yield return Deadline;
            yield return Status;
        }

        public static IEnumerable<string> GetLiveFilteringProperties()
        {
            yield return nameof(HostName);
            yield return nameof(Project);
            //yield return nameof(CpuTime);
            //yield return nameof(CpuTimeRemaining);
            //yield return nameof(LastCheckpoint);
            //yield return nameof(Deadline);
            yield return nameof(Status);
        }
    }
}