// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-01: Injury inspection and per-member town treatment with persistent recovery details (spec 11).
// 2026-11-02: Keep the town service column visible during treatment and use global campaign navigation (spec 14).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Linq;

internal sealed class CompanyRecoveryUI
{
    readonly CompanyCampaignView view;
    readonly CompanyManagementUI ui;
    CompanyCampaign State { get { return view.Campaign; } }
    internal CompanyRecoveryUI(CompanyCampaignView view, CompanyManagementUI ui) { this.view = view; this.ui = ui; }
    internal void Injuries(string id, int page = 0)
    {
        var c = State.Characters.Single(x => x.Id == id); ui.Clear(c.Unit.Name + " — injuries and recovery");
        ui.Text("HP " + c.Unit.Health + "/" + c.Unit.MaxHealth + " | " + CompanyInjuries.Summary(c.Unit) + "\n" +
            (CompanyInjuries.Leader(c.Unit) ? "The leader's serious injuries recover completely; old injuries never remain." : "Minor injuries heal away. Serious injuries become weaker, permanent old injuries."), 30, 140, 1190, 68);
        var injuries = c.Unit.Injuries;
        if (injuries.Count == 0) ui.Text("No injuries. Healthy members gain a little daily sparring experience, active or reserve.", 30, 240, 1160, 80);
        for (int i = 0; i < 5 && page + i < injuries.Count; i++)
            ui.Text(CompanyInjuries.Description(injuries[page + i]), 30, 225 + i * 57, 1170, 54);
        ui.Text("Recovery per day: ordinary 1, camp rest 2, town rest 3. HP also recovers daily.\nSparring requires full lasting HP and no recovering injuries; old injuries alone do not prevent it.", 30, 525, 1180, 55, 16);
        ui.Button("Previous", 30, 590, 150, () => Injuries(id, page - 5), page > 0);
        ui.Button("Next", 195, 590, 150, () => Injuries(id, page + 5), page + 5 < injuries.Count);
        ui.Button("Member", 370, 590, 180, () => ui.Member(id));
        ui.Button("Town treatment", 575, 590, 250, () => Treatment(), !State.Busy && State.Towns.Any(t => t.Tile == State.Position));
    }
    internal void Treatment(int page = 0)
    {
        if (State.Preview || !State.Towns.Any(t => t.Tile == State.Position)) throw new InvalidOperationException("Visit a town for treatment.");
        ui.Clear("Town treatment — gold " + State.Gold, true);
        ui.Text("Pay " + CompanyInjuries.TreatmentCost + " gold per member to shorten their untreated temporary injuries by " + CompanyInjuries.TreatmentDays + " recovery days.\nAt least one recovery day remains. Each injury can be treated once; old injuries cannot be cured.", 30, 140, 1190, 68);
        var members = State.Members.Select(m => State.Characters.Single(c => c.Id == m.CharacterId)).ToArray();
        for (int i = 0; i < 4 && page + i < members.Length; i++)
        {
            var c = members[page + i]; int y = 225 + i * 70;
            ui.Text(c.Unit.Name + " — HP " + c.Unit.Health + "/" + c.Unit.MaxHealth + "\n" + CompanyInjuries.Summary(c.Unit), 30, y, 750, 63);
            ui.Button("Injuries", 800, y, 160, () => Injuries(c.Id));
            ui.Button("Treat: " + CompanyInjuries.TreatmentCost, 985, y, 205, () =>
            {
                try { view.Mutate(() => CompanyInjuries.Treat(State, c.Id)); }
                catch { Treatment(page); throw; }
                Treatment(page);
            }, !State.Busy && State.Gold >= CompanyInjuries.TreatmentCost && CompanyInjuries.CanTreat(c.Unit));
        }
        ui.Button("Previous", 30, 535, 150, () => Treatment(page - 4), page > 0);
        ui.Button("Next", 195, 535, 150, () => Treatment(page + 4), page + 4 < members.Length);
    }
}
