/**
 * Describes within the change detector which strategy will be used the next time change
 * detection is triggered.
 */
export enum ChangeDetectionStrategy {
    /**
     * `CheckedOnce` means that after calling detectChanges the mode of the change detector
     * will become `Checked`.
     */
    CheckOnce,

    /**
     * `Checked` means that the change detector should be skipped until its mode changes to
     * `CheckOnce`.
     */
    Checked,

    /**
     * `CheckAlways` means that after calling detectChanges the mode of the change detector
     * will remain `CheckAlways`.
     */
    CheckAlways,

    /**
     * `Detached` means that the change detector sub tree is not a part of the main tree and
     * should be skipped.
     */
    Detached,

    /**
     * `OnPush` means that the change detector's mode will be set to `CheckOnce` during hydration.
     */
    OnPush,

    /**
     * `Default` means that the change detector's mode will be set to `CheckAlways` during hydration.
     */
    Default,
}